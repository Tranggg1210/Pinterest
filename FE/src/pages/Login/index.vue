<script setup>
import { computed, reactive } from 'vue';
import { RouterLink } from 'vue-router';
import {login} from '../../api/auth.api';
import { validateEmail, validatePassword } from '@/utils/validator';
import { useAuthStore } from '@/stores/auth';
import { useRoute } from 'vue-router';

const message = useMessage();
const authStore = useAuthStore();
const router = useRouter();
const route = useRoute();
const account = reactive({
  email: null,
  password: null
});
const formRef = ref(null);
const rules = {
  email: {
    required: true,
    validator: validateEmail,
    trigger: 'blur'
  },
  password: {
    required: true,
    validator: validatePassword,
    trigger: 'blur'
  }
};
const showForgotPassword = computed(() => {
  if(!account.password) return true;
  else{
    return true;
  }
});
const loginHandler = () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        const { data } = await login({userName: account.email, passowrd: account.passowrd});
        authStore.save({
          ...data
        });
        message.success('Đăng nhập thành công. Xin chào ' + account.email);
        router.push(route.query.redirect || '/');
      } catch (err) {
        if (!!err.response) {
          message.error(err.response.data.message);
        } else {
          message.error(err.message);
        }
      }
    }
  });
};
</script>

<template>
  <div class="login">
    <div class="login__logo">
      <IconBrandPinterest size="36" style="color: red" />
    </div>
    <div class="login__title">
      <p class="login__title-heading">Chào mừng bạn đến với Pinterest</p>
    </div>
    <n-form class="login__wrapper" ref="formRef" :model="account" :rules="rules" size="large">
      <n-form-item path="email" label="Email">
        <n-input
          v-model:value="account.email"
          placeholder="Email"
          class="form-input"
        />
      </n-form-item>
      <n-form-item path="password" label="Mật khẩu" style="margin-top: 4px;">
        <n-input
          v-model:value="account.password"
          placeholder="Mật khẩu"
          type="password"
          show-password-on="click"
          class="form-input"
        />
      </n-form-item>
      <div class="login__wrapper-forgotpass" v-if="showForgotPassword">
        <RouterLink to="/forgot-password">Quên mật khẩu?</RouterLink>
      </div>
      <n-form-item>
        <button type="submit" class="login__wrapper-button button-login" @click="loginHandler">Đăng nhập</button>
      </n-form-item>
      <div class="login__wrapper-infor">
        <p>
          Bằng cách tiếp tục, bạn đồng ý với <br />
          <a href="#" class="login__strong-infor">Điều khoản dịch vụ</a> của Pinterest và xác nhận
          rằng <br />
          bạn đã đọc <a href="#" class="login__strong-infor">Chính sách quyền riêng tư</a> của chúng
          tôi. <br />
          <a href="#" class="login__strong-infor">Thông báo khi thu thập.</a>
        </p>
      </div>
      <div class="login__wrapper-register">
        <p>
          <RouterLink to="/sign-up" class="login__strong-infor"
            >Chưa tham gia Pinterest? Đăng ký</RouterLink
          >
          <br />
        </p>
      </div>
    </n-form>
  </div>
</template>

<!-- css -->
<style lang="scss" scoped>
button {
  border: none;
}

.login {
  width: 484px;
  background: #fff;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px 10px 24px 10px;
  border-radius: 35px;
  margin: 0 auto;

  &__wrapper {
    width: 268px;
  }

  &__title {
    width: 400px;
    height: 80px;
    margin-bottom: 60px;
  }

  &__title-heading {
    font-size: 32px;
    color: #333333;
    text-align: center;
    font-weight: 600;
  }

  &__wrapper-forgotpass {
    font-size: 13px;
    color: #111111;
    font-weight: 450;
    margin: -23px 6px 12px;
    text-align: right;
    a:hover {
      color: #e60023;
    }
  }

  &__wrapper-button {
    margin: 8px 0px 0px;
    padding: 0px 18px;
    text-align: center;
    width: 100%;
    height: 40px;
    border-radius: 999px;

    &.button-login {
      color: #ffffff;
      font-size: 15px;
      background: #e60023;
      display: flex;
      align-items: center;
      justify-content: center;
      font-weight: bold;
      outline: none;
    }
  }

  .form-input {
    border-radius: 12px;
    border: 1px solid #ccc;
    outline: none;
    padding: 5px;
    position: relative;
    margin-bottom: 2px;
  }

  &__wrapper-button {
    &.button-fb {
      color: #ffffff;
      font-size: 16px;
      background: #1877f2;
      display: flex;
      align-items: center;
      justify-content: center;
      letter-spacing: 0.1;
      font-weight: bold;
    }

    &.button-gg {
      color: #111111;
      font-size: 15px;
      background: #f6f6f8;
      display: flex;
      align-items: center;
      justify-content: center;
      font-weight: 500;
      margin: 10px 0 10px 0;
    }

    p {
      width: 88%;
    }
  }

  &__wrapper-infor {
    p {
      text-align: center;
      font-size: 11px;
      color: #767676;
      padding: 10px 0;
    }
  }

  &__strong-infor {
    font-weight: bold;
    text-decoration: none;
    color: #111111;
  }

  &__border {
    border-bottom: 1px solid #e6e6e6;
    margin: 10px auto;
    width: 110px;
  }

  &__wrapper-register {
    font-size: 12px;
    text-align: center;
    color: #333333;
    line-height: 2;

    a:last-child {
      font-weight: 500;
    }
  }
}
</style>

<route lang="yaml">
name: Login
meta:
  layout: default
</route>
