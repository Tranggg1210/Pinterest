<script setup>
import { forgotPassword } from '@/api/auth.api';
import { validateEmail } from '@/utils/validator';
import { useMessage } from 'naive-ui';
import { useRoute, useRouter } from 'vue-router';
const message = useMessage();
const router = useRouter();
const loadingBar = useLoadingBar()
const route = useRoute();

const account = reactive({
  email: null,
});
const formRef = ref(null);
const rules = {
  email: {
    required: true,
    validator: validateEmail,
    trigger: 'blur'
  }
};
const handleForgotPassword = () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        const result = await forgotPassword({email: account.email});
        console.log(result);
        loadingBar.finish()
        if(result?.data?.message === "Email chưa được đăng ký")
        {
          message.warning(result.data.message);
        }
        // router.push(route.query.redirect || '/');
      } catch (err) {
        loadingBar.error()
        console.log(err);
        message.error("Lỗi đăng nhập, kiểm tra mật khẩu");
      }
    }
  });
};
const goBack = () => {
  router.back();
};
</script>

<template>
  <div class="forgot-password container">
    <div class="wide">
      <div @click="goBack">
        <IconArrowLeft class="icon icon-back" />
      </div>
      <h1>Chúng ta hãy tìm tài khoản PixelPalette của bạn</h1>
      <n-form class="form-search" ref="formRef" :model="account" :rules="rules" size="large">
        <n-form-item path="email" label="Email">
          <n-input v-model:value="account.email" placeholder="Email" class="form-input" />
        </n-form-item>
        <n-form-item>
          <button type="submit" class="button-fg" @click="handleForgotPassword">
            Đặt lại mật khẩu
          </button>
        </n-form-item>
      </n-form>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.forgot-password {
  margin-top: 40px;
  h1 {
    font-weight: 500;
    @include mobile {
      font-size: 20px;
    }
  }
  .wide {
    width: 40%;
    @include flex(center, center);
    flex-direction: column;
    @include mobile {
      width: 90%;
    }
    @include small-tablet {
      width: 90%;
    }
    @include tablet {
      width: 70%;
    }
  }
  .form-search {
    width: 80%;
    margin-top: 32px;
    .form-input {
      border-radius: 40px;
      border: 2px solid #ccc;
      outline: none;
      padding: 5px;
      margin-bottom: 2px;
      text-align: left;
    }
    @include mobile() {
      width: 100%;
    }
  }
  .button-fg {
    width: 100%;
    padding: 16px 0;
    background-color: $primary-color;
    border-radius: 50px;
    border: none;
    margin: 0;
    transition: all 0.3s linear;
    color: $white-color;
    outline: none;
    &:hover {
      background-color: $black-color;
    }
    @include mobile {
      width: 100%;
    }
  }

  @include mobile {
    margin-top: 20px;
  }
}
.icon-back {
  position: fixed;
  top: 100px;
  left: 44px;
  width: 44px;
  height: 44px;
  padding: 8px;
  border-radius: 50%;
  transition: all 0.3s linear;
  background-color: #fff;
  z-index: 100;
  &:hover {
    background-color: #ccc;
  }
  @include mobile{
   display: none;
  }
  @include small-tablet{
    top: 86px;
    left: 16px;
  }
  @include tablet{
    left: 24px;
  }
}
</style>
<route lang="yaml">
name: ForgotPassword
meta:
  layout: default
</route>
