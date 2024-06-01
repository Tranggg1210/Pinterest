<script setup>
import { useAuthStore } from '@/stores/auth';
import { useCurrentUserStore } from '@/stores/currentUser';
import { useMessage } from 'naive-ui';
import { useRouter } from 'vue-router';

const user = useAuthStore();
const currentU = useCurrentUserStore();
const message = useMessage();
const router = useRouter();
const handleLogout = () => {
  user.clear();
  currentU.clear();
  message.success('Đăng xuất thành công!');
  router.push('/');
};
</script>
<template>
  <div>
    <nav class="sidebar">
      <router-link to="/admin" class="sidebar-logo">
        <IconBrandPinterest size="36" />
        <h2>PixelPalette</h2>
      </router-link>
      <ul>
        <li>
          <RouterLink to="/admin" exact-active-class="active">
            <IconHome size="24" class="icon" />
            <span>Trang chủ</span>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="/admin-users" exact-active-class="active">
            <IconUsers size="24" class="icon" />
            <span>Quản trị người dùng</span>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="/admin-posts" exact-active-class="active">
            <IconNews size="24" class="icon" />
            <span>Quản trị bài viết</span>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="/admin-notifications" exact-active-class="active">
            <IconBell size="24" class="icon" />
            <span>Quản trị thông báo</span>
          </RouterLink>
        </li>
        <li>
          <RouterLink to="/admin-account" exact-active-class="active">
            <IconUserCircle size="24" class="icon" />
            <span>Quản trị tài khoản</span>
          </RouterLink>
        </li>
        <li @click="handleLogout">
          <div class="logout">
            <IconLogout size="24" class="icon" />
            <span>Đăng xuất</span>
          </div>
        </li>
      </ul>
    </nav>
    <div class="buffer-zone"></div>
  </div>
</template>

<style lang="scss" scoped>
.sidebar {
  background-color: #fff;
  box-shadow: 0 0 8px rgba(0 0 0/20%);
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  z-index: 40;
  width: 273px;
  @include mobile {
    width: 58px;
    padding: 24px 0;
  }
  @include small-tablet {
    width: 60px;
    padding: 8px 0;
  }
  @include tablet {
    width: 72px;
    padding: 0px 0;
  }
}
.sidebar-logo {
  @include flex(center, center);
  color: $primary-color;
  margin-right: 6px;
  border-bottom: 1px solid #ccc;
  padding: 12px 0 !important;
  svg {
    margin-right: 2px !important;
    @include mobile {
      margin: -8px 0 0;
    }
    @include small-tablet {
      margin: 0;
      padding: 0;
    }
    @include tablet {
      margin: 0;
      padding: 0px;
    }
  }
  h2 {
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
    padding: 8px;
    h2 {
      display: none;
    }
  }
  @include tablet {
    h2 {
      display: none;
    }
  }
}
ul {
  margin-top: 1rem;
  padding: 0 24px;
  @include mobile {
    padding: 0;
  }
  @include small-tablet {
    padding: 0;
  }
}
li {
  transition: all 0.3s linear;
  cursor: pointer;
  @include flex(start, center);
  a,.logout {
    padding: 12px 24px;
    @include flex(start, center);
    width: 100%;
    border-radius: 8px;
    @include mobile {
      padding: 12px;
      border-radius: 0;
      justify-content: center;
    }
    @include small-tablet {
      padding: 12px 4px;
      border-radius: 0;
      justify-content: center;
    }
    @include tablet {
      padding: 12px 0px;
      @include flex(center, center);
    }
  }
  &:hover {
    background-color: $primary-color;
    border-radius: 8px;
    opacity: 0.85;
    a {
      color: #fff;
    }
    @include small-tablet {
      border-radius: 0;
    }
    @include mobile {
      border-radius: 0;
    }
  }
}
.logout{
  &:hover{
    color: #fff;
  }
}
span {
  @include mobile {
    display: none;
  }
  @include small-tablet {
    display: none;
  }
  @include tablet {
    display: none;
  }
}
.active {
  background-color: $primary-color;
  color: #fff;
}
.buffer-zone {
  width: 273px!important;
  @include mobile {
    width: 58px !important;
  }
  @include small-tablet {
    width: 60px !important;
  }
  @include tablet {
    width: 72px !important;
  }
}
.icon {
  @include mobile {
    margin-right: 0 !important;
    margin-left: 0 !important;
    width: 22px;
    height: 22px;
  }
  @include tablet {
    margin-right: 0 !important;
    margin-left: 0 !important;
    width: 26px;
    height: 26px;
  }
}
</style>
