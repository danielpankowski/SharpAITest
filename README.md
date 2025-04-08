## Część 2: (CI/CD)
Proces wdrażania jest zarządzany przez github actions. Konfiguracja znajduje się w pliku:
`.github/workflows/deploy.yml`

1. Dodanie pomocniczych zmiennych
2. Trigger - uruchomienie co push na branch main
3. Zdefiniowanie joba build-and-deploy
   * ustawienie systemu operacyjnego na ubuntu
   * udostepnienie kodu na github actions runnerze
   * Konfiguracja .Net 9.x
   * Kompilacja projektu w forme release
   * Publikacja builda
   * Wdrożenie builda do Azure App Serive

## Część 3: Azure
API dostępne pod adresem: http://sharpaitest-api.azurewebsites.net/
Endpoint dostępne pod adresem: http://sharpaitest-api.azurewebsites.net/scalar

### Usługi Azure:
- Azure Resource Manager
- Azure App Service
- Azure SQL Database
- Azure SQL Server

### Połączenie z API:
GET http://sharpaitest-api.azurewebsites.net/api/orders
POST http://sharpaitest-api.azurewebsites.net/api/products

### Konfiguracja
Publish key przechowywany w GitHub secrets;
ConnectionString przechowywany w zmiennych srodowiskowych azure
