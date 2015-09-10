#Acme.Helpers (ASPNET5, MVC6)
![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)

Contains a custom set of Tag Helpers for building ASP.NET 5, MVC 6 and Core applications.

Includes:

* Pagination
* Infinite Scroller
* Table auto-generation
* ...

[Online Demo](http://acme-helpers.azurewebsites.net) / [Download Source](http://github.com/simonray/acme.helpers/zipball/master/)

## Usage

###Pager
```html
<pager asp-for="@Model" show-status=true show-sizes=true />
```
![Alt text](http://s2.postimg.org/4u0fnrxsp/screenshot.png "pager")

###Table

```html
<table asp-for="@Model" ajax="true" class="table" />
```
![Alt text](http://s21.postimg.org/nvy2125fr/ah_table_ss.png "screenshot")

## Change Log

### 1.0.0-beta7 (15-09-02)
* Add &lt;infinite /&gt; TagHelper
