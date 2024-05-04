<template>
    <footer className='footer container'>
      <div className='wide'>
        <div>
          <ul className='footer-nav'>
            <li>
              <RouterLink to='/about-us'>Về chúng tôi</RouterLink>
            </li>
            <li>
              <RouterLink to='/contact'>Liên hệ</RouterLink>
            </li>
            <li>
              <RouterLink to='/login'>Đăng nhập</RouterLink>
            </li>
            <li>
              <RouterLink to='/sign-up'>Đăng ký</RouterLink>
            </li>
          </ul>
          <p className='p-desc'>© Your Website 2024. All Rights Reserved.</p>
        </div>
        <div className='footer-socials'>
          <IconBrandFacebook class="icon"/>
          <IconBrandTwitter class="icon"/>
          <IconBrandInstagram class="icon"/>
        </div>
      </div>
    </footer>
</template>

<script setup>
</script>

<style lang="scss" scoped>
.footer {
  background-color: #f8f9fa;
  padding: 2rem 0 2rem;
  .wide {
    @include mobile {
      flex-direction: column;
    }
    @include small-tablet {
      flex-direction: column;
    }
  }
  &-nav,
  &-socials,
  .wide {
    @include flex(space-between);
  }
  li,
  p {
    padding: 0 1rem;
    text-wrap: nowrap;
    @include mobile {
      padding: 0.5rem;
    }
  }
  a {
    color: $primary-color;
    text-decoration: underline;
    transition: all 0.3s linear;
    &:hover {
      opacity: 0.8;
    }
  }
  p {
    font-size: 1rem;
    text-align: left;
    margin: 1rem 0;
  }

  .icon {
    color: $primary-color;
    transition: all 0.3s linear;
    cursor: pointer;
    margin: 0 16px;
    &:hover {
      opacity: 0.8;
    }
  }
}
</style>
