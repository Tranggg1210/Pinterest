<script setup>

</script>
<template>
    <section className='testimonials container'>
      <div className='wide'>
        <h1 className='h1-title'>Nhóm 6</h1>
        <div className='testimonials-container'>
          <div className='testimonial-item'>
            <div className='avatar'>
              <img src="@/assets/images/ha.jpg" alt='avatar' />
            </div>
            <h3>Trịnh Thu Hà</h3>
          </div>
          <div className='testimonial-item'>
            <div className='avatar'>
              <img src="@/assets/images/hoang.png" alt='avatar' />
            </div>
            <h3>Cao Sỹ Minh Hoàng</h3>
          </div>
          <div className='testimonial-item'>
            <div className='avatar'>
              <img src="@/assets/images/nhat.png" alt='avatar' />
            </div>
            <h3>Cao Đình Nhật</h3>
          </div>
          <div className='testimonial-item'>
            <div className='avatar'>
              <img src="@/assets/images/trang.png" alt='avatar' />
            </div>
            <h3>Nguyễn Thị Trang</h3>
          </div>
          <div className='testimonial-item'>
            <div className='avatar'>
              <img src="@/assets/images/quan.png" alt='avatar' />
            </div>
            <h3>Nguyễn Trung Quân</h3>
          </div>
        </div>
      </div>
    </section>
</template>

<style lang="scss" scoped>
.testimonials {
  padding: 7rem 0;
  h1 {
    text-align: center;
    font-size: 3rem;
    margin: 0 0 3rem;
  }
  .testimonials-container {
    @include flex(center);
    flex-wrap: wrap;
    @include mobile {
      flex-direction: column;
    }
    @include small-tablet {
      flex-direction: column;
    }
  }
  @include mobile {
    padding: 4rem 0rem;
  }
}
.testimonial-item {
  width: 33%;
  margin: 0 0 3rem;
  padding: 0 4rem;
  @include mobile {
    padding: 0 1rem 2.5rem;
    width: 100%;
  }
  @include small-tablet {
    width: 50%;
    margin: 2rem 0;
  }
  .avatar {
    width: 12rem;
    height: 12rem;
    border-radius: 50%;
    overflow: hidden;
    box-shadow: 1px 1px 5px 0 #adb5bd;
    margin: 0 auto;
    img {
      width: 100%;
      object-position: center;
    }
  }
  h3 {
    text-align: center;
    margin: 1.25rem 0 0.5rem;
  }
}
</style>
