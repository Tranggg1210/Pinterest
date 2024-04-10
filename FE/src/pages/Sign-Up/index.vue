<script setup>
import { ref, reactive } from 'vue';
import { register } from '@/api/auth.api';
import { useAuthStore } from '@/stores/auth';
import { validateEmail, validatePassword } from '@/utils/validator';

const message = useMessage();
const loading = ref(false);
const formValue = reactive({
  email: '',
  password: '',
  birthday: '',
  repeatPassword: ''
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
  },
  repeatPassword: {
    required: true,
    validator: (_, repeatPassword) => {
      if (repeatPassword !== formValue.password) {
        return new Error('Mật khẩu không giống với mật khẩu đã nhập lại!');
      }
      return true;
    },
    trigger: ['blur', 'password-input']
  }
};

const registerHandler = () => {};
</script>
<template>
  <div class="sign-up">
    <div class="container">
      <IconBrandPinterest size="40" class="logo" />
      <h1 class="signup-title">Chào mừng bạn đến với</h1>
      <h1 class="signup-title">PixelPalette</h1>
      <p>Tìm những ý tưởng mới để thử</p>

      <form action="" class="form" :model="formValue" :rules="rules" ref="formRef">
        <div class="form-item">
          <label for="email">Email</label>
          <input type="email" v-model="formValue.email" placeholder="Email" />
        </div>
        <div class="form-item">
          <label for="password">Mật khẩu</label>
          <input type="password" v-model="formValue.password" placeholder="Tạo mật khẩu" />
        </div>
        <div class="form-item">
          <label for="password">Mật khẩu vừa nhập</label>
          <input
            type="password"
            v-model="formValue.repeatPassword"
            placeholder="Xác nhận mật khẩu"
          />
        </div>
        <div class="form-item">
          <label for="date">Ngày sinh</label>
          <input type="date" v-model="formValue.birthday" />
        </div>
        <button
          type="submit"
          class="btn-submit pointer"
          :loading="loading"
          @click="registerHandler"
        >
          Tiếp tục
        </button>
      </form>

      <div class="note">
        <p>Bằng cách tiếp tục, bạn đồng ý với</p>
        <p>
          <span>Điều khoản dịch vụ</span> của Pinterest và xác nhận rằng bạn đã đọc
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
  width: 500px;
  border-radius: 25px;
  margin: 100px auto;
  display: flex;
  flex-direction: column;
  align-items: center;
  color: #111111;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%);
}

.signup-title {
  line-height: 1.5;
  font-weight: 500;
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
form {
  width: 300px;
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
}

.pointer {
  cursor: pointer;
}
.form-item {
  display: flex;
  flex-direction: column;

  input {
    border-radius: 15px;
    border: 1px solid #ccc;
    outline: none;
    padding: 13px 15px;
    margin-bottom: 10px;
    margin-top: 5px;

    &:hover {
      box-shadow: 1px 1px 10px rgb(198, 253, 198);
    }
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
  &:hover {
    opacity: 0.7;
    border: 0;
    box-shadow: none;
  }
}

span {
  color: #333333;
}
</style>
<route lang="yaml">
name: SignUp
meta:
  layout: default
</route>
