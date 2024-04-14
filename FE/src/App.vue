<script setup>
const themeOverrides = {
  common: {},
  Button: {
    textColorWarning: '#fff',
    textColorHoverWarning: '#fff',
    textColorFocusWarning: '#fff',
    textColorPressedWarning: '#fff'
  }
};
</script>

<template>
  <n-config-provider :theme-overrides="themeOverrides">
    <n-dialog-provider>
      <n-message-provider>
        <n-notification-provider>
          <n-loading-bar-provider>
            <router-view />
          </n-loading-bar-provider>
        </n-notification-provider>
      </n-message-provider>
    </n-dialog-provider>
  </n-config-provider>
</template>
