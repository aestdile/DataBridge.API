# DataBridge.API

A powerful full-stack tool to transfer and export Excel data into Microsoft SQL Server and XML files with ease. Ideal for data migration, backup and integration workflows.

## 🚀 Features

- **Excel to Database**: Import Excel files directly into Microsoft SQL Server database
- **XML Export**: Export data from database to XML format
- **Full-Stack Solution**: Complete web application with modern UI
- **Data Validation**: Built-in validation for data integrity
- **Bulk Operations**: Handle large datasets efficiently
- **RESTful API**: Clean API endpoints for all operations
- **Comprehensive Testing**: Unit and integration tests included

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core Web API (C#)
- **Database**: Microsoft SQL Server
- **Frontend**: [Framework] (if applicable)
- **Testing**: xUnit/NUnit with comprehensive test coverage
- **File Processing**: EPPlus for Excel operations
- **Data Export**: XML serialization

## 📋 Prerequisites

- .NET 6.0 or later
- Microsoft SQL Server (LocalDB/Express/Full)
- Visual Studio 2022 or Visual Studio Code
- SQL Server Management Studio (recommended)

## ⚡ Quick Start

### 1. Clone the Repository
```bash
git clone https://github.com/aestdile/DataBridge.API.git
cd DataBridge.API
```

### 2. Setup Database
```bash
# Update connection string in appsettings.json
# Run database migrations
dotnet ef database update
```

### 3. Install Dependencies
```bash
dotnet restore
```

### 4. Run the Application
```bash
dotnet run
```

The API will be available at `https://localhost:5001` (or configured port).

## 📁 Project Structure

```
DataBridge.API/
├── Controllers/           # API controllers
├── Models/               # Data models and DTOs
├── Services/             # Business logic services
├── Data/                 # Database context and configurations
├── Tests/                # Unit and integration tests
├── Helpers/              # Utility classes and extensions
├── wwwroot/              # Static files (if applicable)
└── appsettings.json      # Configuration settings
```

## 🔌 API Endpoints

### Excel Import
```http
POST /api/excel/import
Content-Type: multipart/form-data

# Upload Excel file for database import
```

### Data Export
```http
GET /api/data/export/xml
Accept: application/xml

# Export database data as XML
```

### Data Management
```http
GET /api/data              # Get all records
GET /api/data/{id}         # Get specific record
PUT /api/data/{id}         # Update record
DELETE /api/data/{id}      # Delete record
```

## 🧪 Testing

Run all tests:
```bash
dotnet test
```

Run specific test category:
```bash
# Unit tests
dotnet test --filter Category=Unit

# Integration tests
dotnet test --filter Category=Integration
```

### Test Coverage
- Unit Tests: Service layer logic
- Integration Tests: API endpoints
- Data Tests: Database operations
- File Processing Tests: Excel import/XML export

## ⚙️ Configuration

Update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DataBridgeDB;Trusted_Connection=true"
  },
  "FileSettings": {
    "MaxFileSize": "10MB",
    "AllowedExtensions": [".xlsx", ".xls"]
  },
  "ExportSettings": {
    "XmlEncoding": "UTF-8",
    "IncludeSchema": true
  }
}
```

## 📊 Usage Examples

### Import Excel File
```csharp
// C# client example
using var client = new HttpClient();
using var content = new MultipartFormDataContent();
using var fileContent = new ByteArrayContent(fileBytes);
content.Add(fileContent, "file", "data.xlsx");

var response = await client.PostAsync("/api/excel/import", content);
```

### Export to XML
```csharp
// C# client example
var response = await client.GetAsync("/api/data/export/xml");
var xmlContent = await response.Content.ReadAsStringAsync();
```

## 🗃️ Database Schema

The application uses Entity Framework Core with the following main entities:

- **DataRecord**: Main data entity for imported records
- **ImportLog**: Tracks import operations
- **ExportLog**: Tracks export operations

## 🔒 Security Features

- File type validation
- File size limits
- SQL injection prevention
- Input sanitization
- Error handling and logging

## 🐛 Error Handling

The API returns standardized error responses:

```json
{
  "error": "Error description",
  "details": "Additional error details",
  "timestamp": "2024-01-01T00:00:00Z"
}
```

## 📈 Performance Considerations

- Streaming for large file uploads
- Batch processing for database operations
- Asynchronous operations
- Memory-efficient XML generation
- Connection pooling

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Guidelines
- Follow C# coding standards
- Write comprehensive tests for new features
- Update documentation for API changes
- Ensure all tests pass before submitting PR

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

If you encounter any issues or have questions:

1. Check the [Issues](https://github.com/aestdile/DataBridge.API/issues) page
2. Create a new issue with detailed description
3. Include error logs and steps to reproduce

## 🎯 Roadmap

- [ ] Support for additional file formats (CSV, JSON)
- [ ] Real-time data processing
- [ ] Advanced filtering and querying
- [ ] Dashboard for monitoring operations
- [ ] Docker containerization
- [ ] Cloud deployment guides

- [ ] ⭐ Don't forget to star this repository if you found it helpful!

## 📚 Additional Documentation

- [API Documentation](docs/API.md)
- [Database Schema](docs/DATABASE.md)
- [Deployment Guide](docs/DEPLOYMENT.md)
- [Troubleshooting](docs/TROUBLESHOOTING.md)

## ✍️ Muallif
👤 Mukhtor Eshboyev\
🔗 GitHub: [@aestdile](https://github.com/aestdile)\
📌 "When you finish this project, upload it to GitHub and send me the repository link, I'll wait for it!"

---

## License

This project is open-source and available under the MIT License.


## 🌐 Social Networks

<div align="center">
  <a href="https://t.me/aestdile"><img src="https://img.shields.io/badge/Telegram-2CA5E0?style=for-the-badge&logo=telegram&logoColor=white" /></a>
  <a href="https://github.com/aestdile"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white" /></a>
  <a href="https://leetcode.com/aestdile"><img src="https://img.shields.io/badge/LeetCode-FFA116?style=for-the-badge&logo=leetcode&logoColor=black" /></a>
  <a href="https://linkedin.com/in/aestdile"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white" /></a>
  <a href="https://youtube.com/@aestdile"><img src="https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white" /></a>
  <a href="https://instagram.com/aestdile"><img src="https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white" /></a>
  <a href="https://facebook.com/aestdile"><img src="https://img.shields.io/badge/Facebook-1877F2?style=for-the-badge&logo=facebook&logoColor=white" /></a>
  <a href="mailto:aestdile@gmail.com"><img src="https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white" /></a>
  <a href="https://twitter.com/aestdile"><img src="https://img.shields.io/badge/Twitter-1DA1F2?style=for-the-badge&logo=twitter&logoColor=white" /></a>
  <a href="tel:+998772672774"><img src="https://img.shields.io/badge/Phone:+998772672774-25D366?style=for-the-badge&logo=whatsapp&logoColor=white" /></a>
</div>

