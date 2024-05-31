<script setup>
import { ref, reactive } from 'vue';
import { login, register } from '@/api/auth.api';
import { useAuthStore } from '@/stores/auth';
import {
  validateLastName,
  validateFirstName,
  validateEmail,
  validatePassword
} from '@/utils/validator';
import { useRoute, useRouter } from 'vue-router';
import { getCurrentUser } from '@/api/user.api';
import { useCurrentUserStore } from '@/stores/currentUser';
import { checkAdmin } from '@/api/admin.api';
import { useLoadingBar } from 'naive-ui';

const message = useMessage();
const authStore = useAuthStore();
const loadingBar = useLoadingBar();
const router = useRouter();
const route = useRoute();
const currentUser = useCurrentUserStore();
const account = reactive({
  firstName: '',
  lastName: '',
  email: '',
  password: ''
});

const formRef = ref(null);
const rules = {
  firstName: {
    required: true,
    validator: validateFirstName,
    trigger: 'blur'
  },
  lastName: {
    required: true,
    validator: validateLastName,
    trigger: 'blur'
  },
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
const handleFullName = (firstName, lastName) => {
  const fullName = `${lastName} ${firstName} `;
  const formattedFullName = fullName
    .replace(/\s+/g, ' ')
    .trim()
    .replace(/(^|\s)\S/g, (match) => match.toUpperCase());

  return formattedFullName;
};
const registerHandler = () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        await register(account);
        const user = {
          userName: account.email,
          password: account.password
        };
        const result = await login(user);
        authStore.save({
          ...result.data
        });
        const currentUserData = await getCurrentUser();
        const isAdmin = await checkAdmin(currentUserData.id);
        loadingBar.finish();
        message.success('Đăng nhập thành công. Xin chào ' + account.email);
        if(isAdmin?.roles.length === 1 && isAdmin?.roles[0] === 'Member')
        {
          router.push(route.query.redirect || '/');
          if (currentUserData) {
            currentUser.save({
              fullname: handleFullName(currentUserData.firstName, currentUserData.lastName),
              avatar: currentUserData.avatarUrl,
              username: currentUserData.userName
            });
          }
        }else{
          if(isAdmin?.roles.length === 2 && isAdmin.roles.includes('Admin'))
          {
            if (currentUserData) {
            currentUser.save({
              fullname: handleFullName(currentUserData.firstName, currentUserData.lastName),
              avatar: currentUserData.avatarUrl,
              username: currentUserData.userName,
              isAdmin: true
            });
        }
            router.push(route.query.redirect || '/admin');
          }
        }
      } catch (err) {
        loadingBar.error();
        message.error('Đăng ký không thành công');
      }
    }
  });
};
</script>
<template>
  <div class="sign-up">
    <div class="container">
      <IconBrandPinterest size="40" class="logo" />
      <h1 class="signup-title">Chào mừng bạn đến với</h1>
      <h1 class="signup-title">PixelPalette</h1>
      <br /><br />
      <n-form class="login__wrapper" ref="formRef" :model="account" :rules="rules" size="large">
        <n-form-item path="firstName" label="Tên">
          <n-input v-model:value="account.firstName" placeholder="Tên" class="form-input" />
        </n-form-item>
        <n-form-item path="lastName" label="Họ">
          <n-input v-model:value="account.lastName" placeholder="Họ" class="form-input" />
        </n-form-item>
        <n-form-item path="email" label="Email">
          <n-input v-model:value="account.email" placeholder="Email" class="form-input" />
        </n-form-item>
        <n-form-item path="password" label="Mật khẩu" style="margin-top: 4px">
          <n-input
            v-model:value="account.password"
            placeholder="Mật khẩu"
            type="password"
            show-password-on="click"
            class="form-input"
          />
        </n-form-item>
        <n-form-item>
          <button type="submit" class="btn-submit" @click="registerHandler">Đăng ký</button>
        </n-form-item>
      </n-form>
      <div class="note">
        <p>Bằng cách tiếp tục, bạn đồng ý với</p>
        <p>
          <span>Điều khoản dịch vụ</span> của PixelPalette và xác nhận rằng bạn đã đọc
          <span>Chính sách quyền riêng tư </span>của chúng tôi.
        </p>
        <p class="pointer"><span>Thông báo khi thu thập</span></p>
        <p class="confirm pointer">
          Bạn đã là thành viên? <router-link to="/login">Đăng nhập</router-link>
        </p>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.container {
  width: 40%;
  border-radius: 25px;
  margin: 10px auto;
  display: flex;
  flex-direction: column;
  align-items: center;
  color: #111111;
  @include mobile {
    width: 80% !important;
  }
  @include small-tablet {
    width: 60% !important;
  }
}

.signup-title {
  line-height: 1.5;
  font-weight: 500;
  text-align: center;
}
.sign-up {
  height: 100%;
  font-size: 16px;
  position: relative;
}

.logo {
  color: red;
  margin-top: 25px;
  margin-bottom: 10px;
}
.note {
  width: 300px;
  text-align: center;
  color: #767676;
  p {
    line-height: 1;
    font-size: 13px;
    margin: 2px 0px;
  }
  .confirm {
    margin: 30px 0px 40px;
  }
  margin-top: 20px;
  @include mobile {
    width: 90%;
  }
}

.pointer {
  cursor: pointer;
}

.form-input {
  border-radius: 12px;
  border: 1px solid #ccc;
  outline: none;
  padding: 5px;
  position: relative;
  margin-bottom: 2px;
  width: 100%;
  min-width: 280px;
  @include mobile {
    min-width: unset;
  }
}

.btn-submit {
  width: 100%;
  border-radius: 20px;
  background-color: red;
  outline: none;
  border: none;
  color: white;
  font-weight: 700;
  font-size: 16px;
  padding: 13px 0px;
  margin-top: 10px;
  cursor: pointer;
  &:hover {
    opacity: 0.7;
    border: 0;
    box-shadow: none;
  }
}

span {
  color: #333333;
  width: 80;
}
</style>
<route lang="yaml">
name: SignUp
meta:
  layout: default
</route>
