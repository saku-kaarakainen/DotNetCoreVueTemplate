# DotNetCoreVueTemplate
My template for creating .NET Core Vue apps

The app is made with:
- Frontend
  - Vue 3
  - dart-sass
  - babel
  - typescript
  - router
  - vuex
  - eslint
  - axios
- Backend
  - .NET Core 5.0
  - VueCliMiddleware

The app is separated into two different projects inside the solution and both of them can be run separately, but they should be run simultaneously. Visual Studio is the recommended IDE.

This template is loosely based on https://github.com/SoftwareAteliers/asp-net-core-vue-starter. I wanted to make my own template, because I found the template slow and I wanted to try vue3. I am guessing the slowness comes  from the vue middleware binding. I avoid it by keeping both apps (vue SPA and .NET Core) as separate as possible.
