<script setup>
import { ref, reactive } from 'vue';

const input = reactive({
  email: '',
  password: '',
  date: ''
});

const formRef = ref(null);
</script>
<template>
  <div class="sign-up">
    <div class="container">
      <IconBrandPinterest size="40" class="logo" />
      <h1 class="signup-title">Chào mừng bạn đến với</h1>
      <h1 class="signup-title">PixelPalette</h1>
      <p>Tìm những ý tưởng mới để thử</p>

      <form action="" class="form" :model="input">
        <div class="form-item">
          <label for="email">Email</label>
          <input type="email" v-model="input.email" placeholder="Email" />
        </div>
        <div class="form-item">
          <label for="password">Mật khẩu</label>
          <input type="password" v-model="input.password" placeholder="Tạo mật khẩu" />
        </div>
        <div class="form-item">
          <label for="date">Ngày sinh</label>
          <input type="date" v-model="input.date" />
        </div>
        <button type="submit" class="btn-submit pointer">Tiếp tục</button>
      </form>

      <div class="note">
        <p>Bằng cách tiếp tục, bạn đồng ý với</p>
        <p>
          <span>Điều khoản dịch vụ</span> của Pinterest và xác nhận rằng bạn đã đọc
          <span>Chính sách quyền riêng tư </span>của chúng tôi.
        </p>
        <p class="pointer"><span>Thông báo khi thu thập</span></p>
        <p class="confirm pointer">Bạn đã là thành viên? <span> Đăng nhập</span></p>
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
