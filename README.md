# TestProject1

Небольшой .NET 10 проект с юнит-тестом(ами) для демонстрации настроек и запуска тестов.

## Требования

- .NET 10 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/10.0
- Visual Studio 2022/2026 или другой IDE, поддерживающий .NET 10

## Структура репозитория

- TestProject1/ — проект с тестами
- TestProject1/UnitTest1.cs — пример теста
- TestProject1/TestProject1.csproj — файл проекта

## Установка зависимостей

В этом проекте зависимости восстанавливаются через dotnet CLI или через Visual Studio.

Через консоль (PowerShell):

	cd TestProject1
	dotnet restore

Или откройте решение в Visual Studio и выполните восстановление NuGet-пакетов.

Примечание: можно просто открыть решение в Visual Studio и запустить Build (Сборку) или Restore — Visual Studio автоматически восстановит все пакеты и покажет тесты в Test Explorer.

Пакеты NuGet

В проекте обычно указываются ссылки на пакеты в файле `TestProject1.csproj` (PackageReference). Перечисленные вами пакеты часто используются для тестов и автоматизации браузера:

- Selenium.WebDriver
- Selenium.WebDriver.ChromeDriver
- NUnit
- NUnit3TestAdapter
- Microsoft.NET.Test.Sdk

Эти пакеты не хранятся в репозитории в виде бинарников (обычно), а перечисляются в csproj и восстанавливаются при `dotnet restore` или при открытии решения в Visual Studio.

Проверить, установлены ли они в проекте:

1. Откройте `TestProject1/TestProject1.csproj` и найдите элементы `<PackageReference Include="..." />`.
2. Или выполните в консоли внутри папки проекта:

	dotnet list package

Если пакеты отсутствуют, установите их (пример):

	dotnet add package Selenium.WebDriver
	dotnet add package Selenium.WebDriver.ChromeDriver
	dotnet add package NUnit
	dotnet add package NUnit3TestAdapter
	dotnet add package Microsoft.NET.Test.Sdk

В Visual Studio можно установить/удалить пакеты через NuGet Package Manager (правой кнопкой на проект -> Manage NuGet Packages).

## Запуск автотестов

Через dotnet CLI:

	cd TestProject1
	dotnet test

Через Visual Studio:

1. Откройте решение.
2. Откройте Test Explorer (Тестовый обозреватель).
3. Нажмите "Run All" (Запустить все) или отдельные тесты.

## CI / GitHub Actions (пример)

Ниже пример workflow для запуска тестов на GitHub Actions. Создайте файл `.github/workflows/dotnet.yml`:

```yaml
name: .NET

on:
  push:
	branches: [ main ]
  pull_request:
	branches: [ main ]

jobs:
  build:
	runs-on: ubuntu-latest

	steps:
	- uses: actions/checkout@v4
	- name: Setup .NET
	  uses: actions/setup-dotnet@v4
	  with:
		dotnet-version: '10.0.x'
	- name: Restore
	  run: dotnet restore
	- name: Build
	  run: dotnet build --no-restore --configuration Release
	- name: Test
	  run: dotnet test --no-build --verbosity normal
```

Выбор браузера осуществляется, аналогично, через обозреватель тестов путем выбора любого существующего браузера. По умолчанию загрузка осуществляется через Chrome
