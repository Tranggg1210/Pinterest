<script setup>
const themeOverrides = {
  common: {
    baseColor: '#F06C25',
    primaryColor: '#F06C25',
  },
  Input: {
    borderRadius: '0',
    borderHover: 'none',
    borderFocus: 'none',
    borderError: 'none',
    borderHoverError: 'none',
    borderFocusError: 'none',
    boxShadowFocus: 'none',
    boxShadowFocusError: 'none',
    caretColor: 'black'
  },
  Button: {
    textColorWarning: '#fff',
    textColorHoverWarning: '#fff',
    textColorFocusWarning: '#fff',
    textColorPressedWarning: '#fff',
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
