# GovernmentScraper

GovernmentScraper is a C#/.NET crawler that discovers and indexes information from official government websites. The long-term goal is to build a structured database of public officials using first-party government sources.

> **Status:** Early development. The project is currently focused on building a reliable crawler architecture.

## Requirements

```bash
dotnet --version
dotnet --list-sdks
```

* .NET 10 SDK
* Git

## Getting Started

Clone the repository:

```bash
git clone git@github.com:jaryddml/GovernmetScraper.git
cd GovernmetScraper
```

Restore dependencies:

```bash
dotnet restore
```

Run the project:

```bash
dotnet run
```

## Notes

* Seed URLs are stored in `seedurls.txt`.
* The crawler identifies itself with a custom User-Agent and currently waits 2 seconds between requests to avoid overloading websites.
