# GovernmentScraper

GovernmentScraper is a C#/.NET web crawler focused on discovering and indexing information from official government websites. Rather than relying on third-party APIs, the crawler builds its own dataset directly from publicly available government sources.

The long-term objective is to create a structured database of government officials, agencies, and jurisdictions that can eventually power an API, search engine, or analytics platform.

> **Status:** Active early development. The current focus is building a reliable, scalable crawling architecture before implementing advanced official extraction.

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


---

# Current Features

## Intelligent URL Frontier

The crawler maintains a persistent crawl frontier using SQLite and Entity Framework Core.

Features include:

* Persistent crawl queue
* Duplicate URL prevention
* Crawl status tracking
* Automatic URL normalization
* Priority-based crawl ordering

Every discovered URL is stored only once and progresses through a crawl lifecycle.

Example statuses:

* Queued
* Processing
* Visited
* Failed

---

## URL Normalization

Before entering the crawl queue, URLs are normalized to reduce duplicates.

Current normalization includes:

* Relative URL resolution
* Fragment removal (`#section`)
* HTTP/HTTPS validation
* Canonical URL formatting

This ensures pages are not crawled multiple times simply because they appear with different URL forms.

---

## Persistent Database

SQLite is used as the crawler's persistent storage through Entity Framework Core.

Each crawl record stores information such as:

* URL
* Crawl status
* Attempt count
* Discovery timestamp
* Crawl priority

A unique database constraint guarantees that duplicate URLs cannot be inserted.

---

## Priority-Based Crawling

Instead of crawling URLs strictly in discovery order, each discovered link receives a priority score.

Current priority signals include keyword analysis of discovered URLs.

Examples include terms such as:

* mayor
* council
* commissioner
* sheriff
* governor
* department
* board
* directory

Higher-priority pages are processed before lower-priority pages while preserving discovery order among equally ranked URLs.

---

## HTML Fetching

Pages are downloaded using an asynchronous HTTP client.

Current crawler behavior includes:

* Custom User-Agent
* 2-second delay between requests
* Graceful handling of failed requests

Future improvements will include:

* robots.txt support
* adaptive rate limiting
* retry/backoff logic

---

## Link Extraction

The crawler parses downloaded HTML using HtmlAgilityPack.

Current capabilities:

* Extract hyperlinks
* Resolve relative URLs
* Ignore malformed links
* Feed discovered URLs back into the crawl frontier

This creates a continuously expanding crawl graph beginning from the supplied seed URLs.

---

## Government Domain Filtering

Discovered links are filtered to prioritize official government resources.

Current focus is on:

* `.gov`
* state government domains
* local government domains

This keeps the crawler focused on authoritative first-party sources.

---

## Planned Features

The crawler architecture is being designed to support significantly more advanced capabilities, including:

* Page classification
* Official information extraction
* Name/title/email matching
* Jurisdiction detection
* Organization hierarchy detection
* Crawl analytics
* REST API
* Search interface
* Export to JSON and CSV
* Multi-threaded crawling
* Distributed crawling

---

# Technology Stack

* C#
* .NET 10
* Entity Framework Core
* SQLite
* HtmlAgilityPack

---

# Requirements

* .NET 10 SDK
* Git

Verify installation:

```bash
dotnet --version
dotnet --list-sdks
```

---

# Project Structure

```text
GovernmentScraper
├── Frontier
├── PageFetcher
├── LinkParser
├── DotGovFilter
├── PageClassifier
├── DbOperation
├── CrawlerDatabase
├── Models
└── seedurls.txt
```

Each component has a single responsibility:

* **Frontier** manages the crawl queue.
* **PageFetcher** downloads HTML.
* **LinkParser** extracts hyperlinks.
* **DotGovFilter** filters government domains.
* **PageClassifier** assigns crawl priority scores.
* **DbOperation** performs crawler-specific database operations.
* **CrawlerDatabase** manages Entity Framework Core and SQLite.

---

# Project Goals

This project is primarily a learning exercise in building production-style software architecture while creating a genuinely useful tool.

Areas of focus include:

* Web crawling
* Information retrieval
* Software architecture
* Entity Framework Core
* Database design
* Asynchronous programming
* Data extraction
* Scalable crawler design

The end goal is a maintainable crawler capable of discovering, classifying, and extracting structured information from government websites across the United States using first-party public data.
