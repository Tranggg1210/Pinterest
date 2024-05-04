<script setup>

</script>
<template>
    <div class="introduce">
        <div class="introduce-title">
            <h1>Giới thiệu về PixelPalette!!!</h1>
        </div>
        <div class="wide">
            <p>
                PixelPalette là một nền tảng mạng xã hội độc đáo và tạo cảm hứng, mang đến cho người dùng trải nghiệm khám phá và chia sẻ ý tưởng sáng tạo. Tương tự như Pinterest, PixelPalette tập trung vào việc tạo và tổ chức các bảng ý tưởng, nhưng với một cái nhìn riêng biệt.
                Với PixelPalette, bạn có thể tạo ra các bảng ý tưởng đầy màu sắc và sáng tạo bằng cách ghim những hình ảnh, nghệ thuật số, mẫu thiết kế, video và nhiều hơn nữa lên bảng của mình. Bạn có thể tổ chức ý tưởng theo chủ đề, dự án hoặc sở thích cá nhân, tạo ra một không gian sáng tạo để ngắm nhìn và tìm kiếm.
                Với công cụ tìm kiếm mạnh mẽ của PixelPalette, bạn có thể khám phá hàng ngàn ý tưởng mới và sáng tạo từ cộng đồng người dùng. Tìm kiếm theo từ khoá, chủ đề hoặc các nguồn tài nguyên đặc biệt để tìm thấy những hình ảnh và ý tưởng thú vị. Bạn cũng có thể tương tác với các thành viên khác, lưu ý tưởng từ họ và chia sẻ ý tưởng của riêng bạn.
                PixelPalette cũng mang đến một không gian cộng đồng năng động và sáng tạo, nơi bạn có thể kết nối với những người có cùng niềm đam mê và sở thích. Bạn có thể theo dõi người dùng khác, tìm hiểu về nguồn cảm hứng của họ và tham gia vào các cuộc thảo luận sôi nổi.
                Hãy tham gia PixelPalette và khám phá một thế giới đầy màu sắc, nghệ thuật và ý tưởng sáng tạo. Hãy cho sự sáng tạo của bạn tỏa sáng và kết nối với cộng đồng sáng tạo trên PixelPalette ngay hôm nay!
            </p>
            <p>
                Với PixelPalette, chúng tôi không chỉ tạo ra một nền tảng để chia sẻ ý tưởng, mà còn mang đến cho bạn công cụ và tài nguyên hữu ích để triển khai ý tưởng của mình. Bạn có thể khám phá các bài viết, hướng dẫn và tài liệu hữu ích từ cộng đồng người dùng và các chuyên gia trong lĩnh vực sáng tạo. Từ các kỹ thuật nghệ thuật đến các bước thực hiện dự án, PixelPalette sẽ giúp bạn mở rộng kiến thức và kỹ năng của mình.
                Bên cạnh đó, PixelPalette cũng mang đến cho bạn cơ hội thú vị để tham gia vào các cuộc thi và thách thức sáng tạo. Bạn có thể tham gia thiết kế, nhiếp ảnh, và nhiều lĩnh vực khác để thể hiện tài năng và nhận được sự công nhận từ cộng đồng. Đây là cơ hội tuyệt vời để gặt hái cảm hứng mới, kết nối với người mới và khám phá tiềm năng sáng tạo của bản thân.
                Với giao diện người dùng thân thiện, PixelPalette dễ sử dụng và phù hợp với cả người mới bắt đầu và những người có kinh nghiệm. Bạn có thể truy cập từ mọi thiết bị, bất kể là máy tính để bàn, điện thoại di động hoặc máy tính bảng, để truy cập và tương tác với các ý tưởng và cộng đồng ngay cả khi bạn đang di chuyển.
                Hãy đồng hành cùng PixelPalette và truyền cảm hứng, sáng tạo trong mọi khía cạnh cuộc sống của bạn. Khám phá, chia sẻ và kết nối với cộng đồng sáng tạo trên PixelPalette ngay hôm nay, và hãy xem ý tưởng của bạn trở thành hiện thực!
            </p>
        </div>
        <HfFooter/>
    </div>
</template>

<style lang="scss" scoped>
.introduce{
    .introduce-title{
        background: url('@/assets/images/introduce-bg.jpg') no-repeat;
        height: 70vh;
        background-size: cover;
        background-position: center;
        position: relative;
        @include flex;
        &::before{
            content: "";
            position: absolute;
            top: 0;
            right: 0;
            left: 0;
            bottom: 0;
            background-color: #00000094;
            z-index: 4;
        }
    }
    h1{
        font-weight: 600;
        font-size: 4rem;
        text-align: center;
        margin-bottom: 40px;
        color: #fff;
        position: relative;
        z-index: 40;
        @include mobile{
            word-wrap: break-word;
        }
    }
    p{
        width: 80%;
        margin: 28px auto;
        font-size: 20px;
        text-align: justify;
    }
}
</style>
<route lang="yaml">
    name: Introduce
    meta:
      layout: default
</route>
