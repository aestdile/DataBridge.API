class DataBridgeApp {
    constructor() {
        this.apiBaseUrl = "/api/people" // API base URL
        this.init()
    }

    init() {
        this.setupEventListeners()
        this.setupDragAndDrop()
    }

    setupEventListeners() {
        const fileInput = document.getElementById("fileInput")
        const uploadBtn = document.getElementById("uploadBtn")
        const downloadBtn = document.getElementById("downloadBtn")

        fileInput.addEventListener("change", (e) => this.handleFileSelect(e))
        uploadBtn.addEventListener("click", () => this.uploadFile())
        downloadBtn.addEventListener("click", () => this.downloadXML())
    }

    setupDragAndDrop() {
        const uploadArea = document.getElementById("uploadArea")

        uploadArea.addEventListener("dragover", (e) => {
            e.preventDefault()
            uploadArea.classList.add("dragover")
        })

        uploadArea.addEventListener("dragleave", () => {
            uploadArea.classList.remove("dragover")
        })

        uploadArea.addEventListener("drop", (e) => {
            e.preventDefault()
            uploadArea.classList.remove("dragover")

            const files = e.dataTransfer.files
            if (files.length > 0) {
                this.handleFileSelect({ target: { files } })
            }
        })
    }

    handleFileSelect(event) {
        const file = event.target.files[0]
        if (!file) return

        // Validate file type
        const validTypes = ["application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/vnd.ms-excel"]

        if (!validTypes.includes(file.type) && !file.name.match(/\.(xlsx|xls)$/i)) {
            this.showToast("Faqat Excel fayllari (.xlsx, .xls) qabul qilinadi!", "error")
            return
        }

        // Show file info
        this.displayFileInfo(file)
    }

    displayFileInfo(file) {
        const fileInfo = document.getElementById("fileInfo")
        const fileName = document.getElementById("fileName")
        const fileSize = document.getElementById("fileSize")
        const uploadArea = document.getElementById("uploadArea")

        fileName.textContent = file.name
        fileSize.textContent = this.formatFileSize(file.size)

        uploadArea.style.display = "none"
        fileInfo.style.display = "flex"

        // Store file for upload
        this.selectedFile = file
    }

    formatFileSize(bytes) {
        if (bytes === 0) return "0 Bytes"
        const k = 1024
        const sizes = ["Bytes", "KB", "MB", "GB"]
        const i = Math.floor(Math.log(bytes) / Math.log(k))
        return Number.parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + " " + sizes[i]
    }

    async uploadFile() {
        if (!this.selectedFile) {
            this.showToast("Iltimos, avval fayl tanlang!", "warning")
            return
        }

        const formData = new FormData()
        formData.append("file", this.selectedFile)

        this.showLoading(true)
        this.showProgress(0)

        try {
            const response = await fetch(`${this.apiBaseUrl}/upload-data`, {
                method: "POST",
                body: formData,
            })

            if (!response.ok) {
                const errorData = await response.json()
                throw new Error(errorData.Error || "Fayl yuklashda xatolik yuz berdi")
            }

            const result = await response.json()
            this.showProgress(100)

            setTimeout(() => {
                this.showLoading(false)
                this.showResults(result)
                this.showToast(`Muvaffaqiyatli! ${result.length} ta yozuv yuklandi.`, "success")
            }, 500)
        } catch (error) {
            this.showLoading(false)
            this.showToast(`Xatolik: ${error.message}`, "error")
            console.error("Upload error:", error)
        }
    }

    async downloadXML() {
        this.showLoading(true)

        try {
            const response = await fetch(`${this.apiBaseUrl}/download-data`, {
                method: "GET",
            })

            if (!response.ok) {
                throw new Error("XML faylni yuklab olishda xatolik yuz berdi")
            }

            const blob = await response.blob()
            const url = window.URL.createObjectURL(blob)
            const a = document.createElement("a")
            a.href = url
            a.download = "PeopleWithPets.xml"
            document.body.appendChild(a)
            a.click()
            window.URL.revokeObjectURL(url)
            document.body.removeChild(a)

            this.showLoading(false)
            this.showToast("XML fayl muvaffaqiyatli yuklab olindi!", "success")
        } catch (error) {
            this.showLoading(false)
            this.showToast(`Xatolik: ${error.message}`, "error")
            console.error("Download error:", error)
        }
    }

    showResults(data) {
        const resultsSection = document.getElementById("resultsSection")
        const resultsContent = document.getElementById("resultsContent")

        // XML formatida ma'lumotlarni yaratish - C# formatiga mos
        let xmlContent = `<?xml version="1.0" encoding="UTF-8"?>
<ArrayOfPerson xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">`

        data.forEach((item) => {
            xmlContent += `
  <Person>
    <Id>${item.person.id}</Id>
    <Name>${this.escapeXml(item.person.name)}</Name>
    <Age>${item.person.age}</Age>
    <Pets>`

            item.pets.forEach((pet) => {
                xmlContent += `
      <Pet>
        <Id>${pet.id}</Id>
        <Name>${this.escapeXml(pet.name)}</Name>
        <Type>${this.escapeXml(pet.type)}</Type>
        <PersonId>${pet.personId}</PersonId>
      </Pet>`
            })

            xmlContent += `
    </Pets>
  </Person>`
        })

        xmlContent += `
</ArrayOfPerson>`

        // XML ni syntax highlighting bilan ko'rsatish
        const highlightedXml = this.highlightXmlSyntax(xmlContent)

        const totalPets = data.reduce((total, item) => total + item.pets.length, 0)

        const html = `
  <div class="results-summary">
    <h4><i class="fas fa-chart-bar"></i> Ma'lumotlar Xulosasi</h4>
    <div class="summary-grid">
      <div class="summary-item">
        <div class="summary-number">${data.length}</div>
        <div class="summary-label">Jami Odamlar</div>
      </div>
      <div class="summary-item">
        <div class="summary-number">${totalPets}</div>
        <div class="summary-label">Jami Uy Hayvonlari</div>
      </div>
      <div class="summary-item">
        <div class="summary-number">${new Date().toLocaleDateString("uz-UZ")}</div>
        <div class="summary-label">Yaratilgan Sana</div>
      </div>
    </div>
  </div>
  
  <div class="xml-container">
    <div class="xml-header">
      <h4><i class="fas fa-code"></i> ArrayOfPerson XML Natija</h4>
      <button class="copy-btn" onclick="navigator.clipboard.writeText(\`${this.escapeForAttribute(xmlContent)}\`).then(() => app.showToast('XML muvaffaqiyatli nusxalandi!', 'success')).catch(() => app.showToast('Nusxalashda xatolik yuz berdi!', 'error'))">
        <i class="fas fa-copy"></i> Nusxalash
      </button>
    </div>
    <div class="xml-content">${highlightedXml}</div>
  </div>
`

        resultsContent.innerHTML = html
        resultsSection.style.display = "block"
        resultsSection.scrollIntoView({ behavior: "smooth" })
    }

    // XML syntax highlighting uchun yangi metod qo'shing
    highlightXmlSyntax(xml) {
        return xml
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/(&lt;\/?)([\w-]+)/g, '<span class="xml-tag">$1$2</span>')
            .replace(/(&gt;)/g, '<span class="xml-tag">$1</span>')
            .replace(/([\w-]+)(=)/g, '<span class="xml-attr">$1</span>$2')
            .replace(/(=")([^"]*?)(")/g, '=<span class="xml-value">"$2"</span>')
            .replace(/(&lt;[^&]*?&gt;)([^&<]+?)(&lt;)/g, '$1<span class="xml-text">$2</span>$3')
    }

    // XML ni escape qilish uchun metod
    escapeXml(text) {
        if (typeof text !== "string") return text
        return text
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&apos;")
    }

    // Attribute uchun escape
    escapeForAttribute(text) {
        return text.replace(/'/g, "\\'").replace(/"/g, '\\"')
    }

    // Clipboard ga nusxalash metodi qo'shing
    copyXmlToClipboard(xmlContent) {
        navigator.clipboard
            .writeText(xmlContent)
            .then(() => {
                this.showToast("XML muvaffaqiyatli nusxalandi!", "success")
            })
            .catch(() => {
                this.showToast("Nusxalashda xatolik yuz berdi!", "error")
            })
    }

    showProgress(percent) {
        const progressContainer = document.getElementById("progressContainer")
        const progressFill = document.getElementById("progressFill")
        const progressText = document.getElementById("progressText")

        if (percent > 0) {
            progressContainer.style.display = "block"
            progressFill.style.width = percent + "%"
            progressText.textContent = percent + "%"
        } else {
            progressContainer.style.display = "none"
        }
    }

    showLoading(show) {
        const loadingOverlay = document.getElementById("loadingOverlay")
        loadingOverlay.style.display = show ? "flex" : "none"
    }

    showToast(message, type = "info") {
        const toastContainer = document.getElementById("toastContainer")
        const toast = document.createElement("div")
        toast.className = `toast ${type}`

        const icon =
            type === "success"
                ? "check-circle"
                : type === "error"
                    ? "exclamation-circle"
                    : type === "warning"
                        ? "exclamation-triangle"
                        : "info-circle"

        toast.innerHTML = `
            <i class="fas fa-${icon}"></i>
            <span>${message}</span>
        `

        toastContainer.appendChild(toast)

        // Auto remove after 5 seconds
        setTimeout(() => {
            toast.style.animation = "slideInRight 0.3s ease reverse"
            setTimeout(() => {
                if (toast.parentNode) {
                    toast.parentNode.removeChild(toast)
                }
            }, 300)
        }, 5000)
    }
}

// Additional CSS for results table
const additionalCSS = `
.results-summary {
    background: rgba(102, 126, 234, 0.1);
    padding: 20px;
    border-radius: 10px;
    margin-bottom: 20px;
}

.results-summary h4 {
    color: #333;
    margin-bottom: 10px;
    display: flex;
    align-items: center;
    gap: 10px;
}

.results-table {
    margin-top: 20px;
}

.results-table h5 {
    color: #333;
    margin-bottom: 15px;
}

.table-container {
    overflow-x: auto;
    border-radius: 10px;
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
}

table {
    width: 100%;
    border-collapse: collapse;
    background: white;
}

th, td {
    padding: 12px 15px;
    text-align: left;
    border-bottom: 1px solid #eee;
}

th {
    background: linear-gradient(135deg, #667eea, #764ba2);
    color: white;
    font-weight: 600;
}

tr:hover {
    background: rgba(102, 126, 234, 0.05);
}

tr:last-child td {
    border-bottom: none;
}
`

// Add additional CSS to the page
const style = document.createElement("style")
style.textContent = additionalCSS
document.head.appendChild(style)

// Initialize the app when DOM is loaded
let app
document.addEventListener("DOMContentLoaded", () => {
    app = new DataBridgeApp()
})
