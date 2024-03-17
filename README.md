# PINTEREST

Clone social networking site website using Vite + Vue3 + APS.NET and many features.

## Project Setup

```sh
yarn
```

### Compile and Hot-Reload for Development

```sh
yarn dev
```

### Compile and Minify for Production

```sh
yarn build
```

## Features

- **Framework**: [Vue](https://vuejs.org/)
- **Router**: [Vue Router](https://router.vuejs.org/)
- **State Management**: [Pinia](https://pinia.vuejs.org/)
- **Style**: [Scss](https://sass-lang.com/)
- **Icon**: [Tabler](https://tabler-icons.io/)
- **Vite plugin**: [unplugin-auto-import](https://github.com/antfu/unplugin-auto-import), [unplugin-vue-components](https://github.com/antfu/unplugin-vue-components), [vite-plugin-pages](https://github.com/hannoeru/vite-plugin-pages), [vite-plugin-vue-layouts](https://github.com/JohnCampionJr/vite-plugin-vue-layouts)
- **UI**: [NaiveUI](https://www.naiveui.com/)
- **Http clent**: [Axios](https://axios-http.com/)

## Kết nối với sql server
### Mở SQL Server 2022 Configuration
    + Mở thư mục  SQL Server Network Configuration
    + Chỉnh sửa TCP/IP -> Enable
    + Chỉnh sửa Port trong TCP/IP -> 1433
### Trong .env chỉnh sửa
    + DB_CONNECTION=sqlsrv
    + DB_HOST=DESKTOP-QTRJVTE\SQLEXPRESS
    + DB_PORT=1433
    + DB_DATABASE=pinterestdb
    + DB_USERNAME=
    + DB_PASSWORD=
    
    + Với DB_DATABASE là tên database dự án , DB_HOST là server name (Tên máy \ "server.Có thể mở SQL server coppy tên )
## Cài đặt laravel
    + Composer create-project laravel/laravel Laravel
## Chạy laravel
    + php artisan serve
## Đối với .env
    + Không thực hiện ẩn .env trong dự án
