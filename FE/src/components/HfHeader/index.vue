<script setup>
import { useAuthStore } from '@/stores/auth';
const user = useAuthStore();
const router = useRouter();
const show = ref(false);
const showSearchModel = ref(false);
const isScrolled = ref(false);

const handleScroll = () => {
  isScrolled.value = window.scrollY >= 160;
};

onMounted(() => {
  window.addEventListener('scroll', handleScroll);
});

onBeforeUnmount(() => {
  window.removeEventListener('scroll', handleScroll);
});
const options = ref([
  {
    label: 'Thông tin cá nhân',
    key: '/user-infor'
  },
  {
    label: 'Hồ sơ của bạn',
    key: '/profile-favorite'
  },
  {
    label: 'Đăng xuất',
    key: 'logout'
  }
]);
const goToPage = (key) => {
  if (key === 'logout') {
    user.clear();
    message.success('Đăng xuất thành công!');
    router.push('/');
  } else {
    router.push(key);
  }
};
</script>
<template>
  <div
    :class="{
      header: true,
      container: true,
      'header-shadow': isScrolled
    }"
  >
    <div class="wide">
      <div style="display: flex; justify-content: center; align-items: center">
        <router-link to="/" class="header-logo">
          <IconBrandPinterest size="36" />
          <h2 v-if="!user.loggedIn">PixelPalette</h2>
        </router-link>
        <router-link class="btn-home" to="/" exact-active-class="active">
          <HfButton> Trang chủ </HfButton>
        </router-link>
        <router-link class="btn-home" to="/posts" exact-active-class="active" v-if="user.loggedIn">
          <HfButton> Tạo </HfButton>
        </router-link>
      </div>
      <div class="header-search" @click="showSearchModel = true">
        <IconSearch size="20px" />
        <input type="text" placeholder="Tìm kiếm..." />
      </div>
      <div class="btn-container" v-if="!user.loggedIn">
        <router-link to="/login" exact-active-class="active">
          <HfButton class="btn-login"> Đăng nhập </HfButton>
        </router-link>
        <router-link to="/sign-up" exact-active-class="active">
          <HfButton class="btn-signup"> Đăng ký </HfButton>
        </router-link>
      </div>
      <IconMenu2 size="28" class="mobile-menu-icon" @click="show = true" style="margin-left: 12px;" />
      <n-drawer v-model:show="show" width="70%">
        <n-drawer-content closable>
          <template #header>
            <div style="display: flex; align-items: center">
              <IconBrandPinterest size="28" style="color: #e60023" />
              Menu
            </div>
          </template>
          <div class="mobile-menu">
            <router-link to="/login" exact-active-class="active" v-if="!user.loggedIn">
              <HfButton class="btn-login"> Đăng nhập </HfButton>
            </router-link>
            <router-link to="/sign-up" exact-active-class="active" v-if="!user.loggedIn">
              <HfButton class="btn-signup"> Đăng ký </HfButton>
            </router-link>
            <router-link to="/" exact-active-class="active" v-if="user.loggedIn">
              <HfButton> Trang chủ </HfButton>
            </router-link>
            <router-link to="/post" exact-active-class="active" v-if="user.loggedIn">
              <HfButton> Tạo </HfButton>
            </router-link>
            <router-link to="/abc" exact-active-class="active" v-if="user.loggedIn">
              <HfButton> Tin nhắn </HfButton>
            </router-link>
            <router-link to="/notification" exact-active-class="active" v-if="user.loggedIn">
              <HfButton> Thông báo </HfButton>
            </router-link>
            <router-link to="/profile-favorite" exact-active-class="active" v-if="user.loggedIn">
              <HfButton> Hồ sơ của bạn </HfButton>
            </router-link>
            <router-link to="/sign-up" exact-active-class="active" v-if="user.loggedIn">
              <HfButton> Đăng xuất </HfButton>
            </router-link>
          </div>
        </n-drawer-content>
      </n-drawer>
      <div class="menu-logined" v-if="user.loggedIn">
        <img src="@/assets/images/notification.png" alt="notification" title="notification" 
          class="menu-logined-icon" @click="() => goToPage('/notification')"/>
        <img src="@/assets/images/messenger.png" alt="message" title="message" class="menu-logined-icon"/>
        <n-dropdown v-if="user.loggedIn" :options="options" show-arrow @select="goToPage">
          <div
            style="
              display: flex;
              justify-content: space-between;
              align-items: center;
              cursor: pointer;
            "
            @click="() => goToPage('/profile-favorite')"
          >
            <img src="@/assets/images/user-avatar.png" alt="avatar" class="user-avatar" />
            <IconChevronDown/>
          </div>
        </n-dropdown>
      </div>
    </div>

  </div>
  <div class="cushion"></div>
</template>

<style lang="scss" scoped src="./HfHeader.scss"></style>
