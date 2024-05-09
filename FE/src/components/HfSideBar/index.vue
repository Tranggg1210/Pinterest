<script setup>
</script>
<template>
    <nav class="sidebar">
        <router-link to="/admin" class="sidebar-logo">
          <IconBrandPinterest size="36" />
          <h2>PixelPalette</h2>
        </router-link>
        <ul>
            <li>
                <RouterLink to="/admin" exact-active-class="active">
                    <IconHome size="24"/>
                    <span>Trang chủ</span>
                </RouterLink>
            </li>
            <li>
                <RouterLink to="/admin-posts" exact-active-class="active">
                    <IconUsers size="24"/>
                    <span>Quản trị người dùng</span>
                </RouterLink>
            </li>
            <li>
                <RouterLink to="/admin-users" exact-active-class="active">
                    <IconNews size="24"/>
                    <span>Quản trị bài viết</span>
                </RouterLink>
            </li>
        </ul>
    </nav>
    <div class="buffer-zone">

    </div>
</template>


<style lang="scss" scoped>
.sidebar{
    background-color: #fff;
    box-shadow: 0 0 8px rgba(0 0 0/20%);
    position: fixed;
    top: 0;
    left: 0;
    bottom: 0;
    z-index: 40;
    width:18%;
    @include mobile{
      padding: 24px 0;
    }
    @include small-tablet{
      width: 10%;
      padding: 24px 0;
    }
    @include tablet{
      width: 20%;
    }
}
.sidebar-logo {
  @include flex(center, center);
  color: $primary-color;
  margin-right: 6px;
  border-bottom: 1px solid #ccc ;
  padding: 12px 0;
  svg {
    margin-right: 2px !important;
    @include mobile{
      margin-top: -8px;
    }
    @include tablet{
      display: none;
    }
  }
  h2{
    text-transform: uppercase;
  }

  @include mobile {
    padding: 0 8px;
    justify-content: center;
    h2 {
      display: none;
    }
  }
  @include small-tablet {
    justify-content: center;
    padding: 0 8px;
    h2 {
      display: none;
    }
  }
}
ul{
    margin-top: 2rem;
    padding:0 24px;
}
li{
    transition: all 0.3s linear;
    cursor: pointer;
    @include flex(start, center);
    a{
        padding: 12px 24px;
        @include flex(start, center);
        width: 100%;
        border-radius: 8px;
        @include mobile{
          padding: 12px;
          border-radius: 0;
          justify-content: center;
        }
        @include small-tablet{
          padding: 12px 4px;
          border-radius: 0;
          justify-content: center;
        }
        @include tablet{
          padding: 12px 20px;
        }
    }
    &:hover{
        background-color: $primary-color;
        border-radius: 8px;
        opacity: 0.85;
        a{
            color: #fff;
        }
    }
}
span{
  @include mobile{
    display: none;
  }
  @include small-tablet{
    display: none;
  }
}
.active{
    background-color: $primary-color;
    color: #fff;
}
.buffer-zone{
  display: inline;
  width:18%;
  @include small-tablet{
    width: 10%;
  }
  @include tablet{
      width: 20%;
  }
}
</style>
