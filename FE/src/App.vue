<script setup>
const themeOverrides = {
  common: {
    baseColor: '#e60023',
    primaryColor: '#e60023',
  },
  Button: {
    textColorWarning: '#fff',
    textColorHoverWarning: '#fff',
    textColorFocusWarning: '#fff',
    textColorPressedWarning: '#fff'
  },
  Input: {
    border:'#e60023',
    borderFocus: '#e60023',
    borderHover: '#e60023',
  },
  InternalSelection: {
    paddingSingle: '8px 36px 8px 12px',
    borderFocus: '#e60023',
    borderHover: '#e60023',
  }
  // Select:{
  //   border: '#e60023',
  //   borderHover:'#e60023'
  // }
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
