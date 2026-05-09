#  SillyCats

> *A curated collection of internet legends, feline disasters, and chronically unhinged creatures.*

A simple ASP.NET Core MVC web application built as a first assignment for the **Software Architecture** course at Instituto Tecnológico de Software. It implements a basic catalog with listing, filtering, detail view, and item creation — styled with a comic book aesthetic.

---

##  Screenshots

Are in the carpet named Docs

---

##  Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| Language | C# |
| Views | Razor (`.cshtml`) |
| Styling | Bootstrap 5 + Custom CSS |
| Fonts | [Bangers](https://fonts.google.com/specimen/Bangers) · [Comic Neue](https://fonts.google.com/specimen/Comic+Neue) (Google Fonts) |
| IDE |  Rider |

---

##  Project Structure

```
SillyCats/
├── Controllers/
│   ├── HomeController.cs       # Landing & error pages
│   └── CatalogController.cs    # Catalog CRUD logic
├── Models/
│   ├── Item.cs                 # Cat entity
│   └── ErrorViewModel.cs
├── Views/
│   ├── Catalog/
│   │   ├── Index.cshtml        # Cat grid with filters
│   │   ├── Details.cshtml      # Individual cat profile
│   │   └── Add.cshtml          # Add new cat form
│   ├── Home/
│   │   └── Index.cshtml        # Landing page
│   └── Shared/
│       └── _Layout.cshtml      # Base layout & navbar
└── wwwroot/
    └── css/site.css            # Comic book theme
```

---

##  Running Locally

**Prerequisites:** .NET 10 SDK

```bash
# Clone the repo
git clone https://github.com/<your-username>/ArqSoft-S01-JuanZapata.git
cd ArqSoft-S01-JuanZapata

# Run
dotnet run --project SillyCats

# Open in browser
# http://localhost:5056
```

---

##  Features

- **Catalog listing** — grid of all registered cats
- **Filter by name** — quick filter bar at the top of the catalog
- **Detail view** — full profile page per cat
- **Add new cats** — form to register a new entry to the catalog
- **Comic book UI** — Ben-Day dot background, Bangers font, panel-style cards, offset box shadows

---

##  Author

**Juan Zapata** · TSU Software Engineering  
Tecnológico de Software  
[![LinkedIn](https://img.shields.io/badge/LinkedIn-blue?logo=linkedin&logoColor=white)](https://www.linkedin.com/in/juan-jos%C3%A9-zapata-buenfil/)
[![GitHub](https://img.shields.io/badge/GitHub-black?logo=github&logoColor=white)](https://github.com/Colcolat)

---

*Arquitectura de Software — Q3 2026 · TSW*
*Gemini was used for the creation of the Readme document and Claude was used for the frontend of the page*
