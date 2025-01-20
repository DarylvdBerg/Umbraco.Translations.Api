# Umbraco.Translation.Api

## About
Since Umbraco 13, they've introduces the Delivery API that allows us to fetch content and use Umbraco in a headless scenario.
One part that is currently missing, or rather not implemented in a satisfying way, is the ability to fetch translations.

Currently Umbraco does offer fetching translations through their management API, but that would expose a bit more that we would like.
This package serves as a small addition to a project, giving you the ability to only fetch translations.

## Usage
- Install package
- Add configuration (See configuration section)
- Enjoy

## Configuration
To get the basics up and running you'll need to add a `TranslationApi` configuration settings in your `appsettings.json`.
You're able to configure an API key to your liking if you don't want to expose the endpoints publically.

This package contains functionality to cache the translations, for this you'll have the following two options to configure for `CacheStrategy`:
- UmbracoCacheStrategy (Makes use of built in umbraco run time cache)
- RedisCacheStrategy (Allows you to use your own redis cache server)

If you've chosen the lather option for caching, you'll also need to provide an `RedisCacheConfiguration` section inside your `TranslationApi` section.
Here you'll have to configure the `ConnectionString` for your redis server.

## Functionality
- Provides an endpoint for fetching a translation based on key and culture
- Provides functionality for caching the fetched object.
- Provides an notification handler out of the box, that will clear the cached item each time a translation is updated.

## Roadmap
- [x] Implement caching strategies
- [x] Implement an endpoint for fetching a translation based by key and culture
- [x] Implement a notification handler that will clear the cache each time a translation is updated
- [x] Implement optional endpoint authorization
- [ ] Test functionality through, increase test coverage (yes I know ;))
- [ ] Add and configure CI/CD including publishing package
- [ ] Update package for Umbraco 14 / 15 support 
