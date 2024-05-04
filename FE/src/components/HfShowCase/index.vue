<script setup>

</script>
<template>
    <section className='show-case'>
      <div className='show-case-item'>
        <div className='show-case-content'>
          <h1 className='h1-title'>Tìm kiếm và khám phá ý tưởng</h1>
          <p className='p-desc'>
            Cung cấp một công cụ tìm kiếm mạnh mẽ cho phép người dùng khám phá các ý tưởng mới dựa trên các chủ đề, từ khoá hoặc hình ảnh liên quan. Người dùng có thể tìm kiếm các bảng ý tưởng, pin và người dùng khác, và lưu các ý tưởng mà họ thích vào các bảng ý tưởng của mình.
          </p>
        </div>
        <div className='show-case-img'>
          <img src="@/assets/images/showcase1.webp" alt='show case image 1' />
        </div>
      </div>
      <div className='show-case-item'>
        <div className='show-case-img'>
          <img src="@/assets/images/showcase2.webp" alt='show case image 1' />
        </div>
        <div className='show-case-content'>
          <h1 className='h1-title'>Chia sẻ và tương tác xã hội</h1>
          <p className='p-desc'>
            Người dùng có thể chia sẻ các ý tưởng của mình bằng cách đăng tải hình ảnh, video hoặc liên kết lên PixelPalette. Họ cũng có thể tương tác với những người dùng khác bằng cách theo dõi, lưu các ý tưởng từ người dùng khác và tương tác qua việc thích, bình luận hoặc chia sẻ các pin.
          </p>
        </div>
      </div>
      <div className='show-case-item'>
        <div className='show-case-content'>
          <h1 className='h1-title'>Tạo ý tưởng (boards)</h1>
          <p className='p-desc'>
            Người dùng có thể tạo các ý tưởng để lưu trữ và tổ chức các hình ảnh, video và liên kết mà họ quan tâm. Mỗi bảng ý tưởng có thể chứa nhiều pin (điểm ghim) và người dùng có thể tạo nhiều bảng ý tưởng khác nhau dựa trên các chủ đề riêng biệt.
          </p>
        </div>
        <div className='show-case-img'>
          <img src="@/assets/images/showcase3.webp" alt='show case image 1' />
        </div>
      </div>
    </section>
</template>

<style lang="scss" scoped>
.show-case {
  .show-case-item {
    @include flex;
    @include mobile {
      flex-direction: column;
    }
    @include small-tablet {
      flex-direction: column;
    }
    .show-case-content,
    .show-case-img {
      width: 50%;
      height: 100%;
      img{
        width: 100%;
        height: 72vh !important;
        object-fit: cover;
      }
      @include mobile {
        width: 100%;
      }
      @include small-tablet {
        width: 100%;
      }
    }
    .show-case-content {
      padding: 7rem;
      @include mobile {
        padding: 2rem;
      }
      @include tablet {
        padding: 1rem 2rem;
      }
    }
    img {
      width: 100%;
      height: 100%;
      margin-bottom: -3px;
    }
    p {
      text-align: left;
    }
  }

  @include mobile {
    .show-case-item:nth-child(1) {
      .show-case-content {
        order: 1;
      }
    }
    .show-case-item:nth-child(3) {
      .show-case-content {
        order: 2;
      }
    }
  }
  @include small-tablet {
    .show-case-item:nth-child(1) {
      .show-case-content {
        order: 1;
      }
    }
    .show-case-item:nth-child(3) {
      .show-case-content {
        order: 2;
      }
    }
  }
}
</style>
