# RUN
    + Chạy composer install
    + Chỉnh sửa env
    + Chạy php artisan serve
## API
    + Swagger : {url}/api/documentation
## Search
    + Cài composer require algolia/algoliasearch-client-php
    + Cài composer require laravel/scout
    + Cài composer require algolia/scout-extended
## Laravel websocket 
    link : https://beyondco.de/docs/laravel-websockets/getting-started/installation
    + Cài đặt composer require beyondcode/laravel-websockets .Nếu phát sinh lỗi thêm "-W" ở cuối

    + Chạy server websocket
        1. php artisan websockets:serve
        2. Truy cập {url}/laravel-websockets
    + Chạy client websocket
        1. Cài đặt laravel echo : npm install --save laravel-echo pusher-js
        2.Cấu hình vào js tổng (ví dụ app.js)
            import Echo from 'laravel-echo';

            window.Pusher = require('pusher-js');

            // Cấu hình Pusher
            Pusher.logToConsole = true;

            window.Echo = new Echo({
                broadcaster: 'pusher',
                key: 'PixelPaletteKey',
                cluster: 'mt1',
                encrypted: true
            });

            
        3. 

