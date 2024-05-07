<script setup>
import { onBeforeMount, reactive } from 'vue';
import { validateDOB, validatePassword } from '@/utils/validator';
import router from '@/router';
import { useAuthStore } from '@/stores/auth';
import { getCurrentUser } from '@/api/user.api';


const message = useMessage();
const auth = useAuthStore();
const loadingBar = useLoadingBar();
const disabledRef = ref(true);
const dialog = useDialog();
const userForm = ref({});
const user = ref({});
const account = reactive({
  username: '',
  oldPassword: '',
  password: '',
  repeatPassword: ''
});

const formRef = ref(null);
const formRefAccount = ref(null);
const rules = {
  fullName: {
    required: true,
    trigger: 'blur'
  },
  dob: {
    required: true,
    validator: validateDOB,
    trigger: ['input', 'blur']
  }
};
const rulesAccount = {
  oldPassword: {
    required: true,
    validator: validatePassword,
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
      if (repeatPassword !== account.password) {
        return new Error('Mật khẩu không giống với mật khẩu đã nhập lại!');
      }
      return true;
    },
    trigger: ['blur', 'password-input']
  }
};
const loadUser = async() => {
  try {
    const {data} = await getCurrentUser();
    console.log(data);
  } catch (error) {
    console.log(error);
  }
}

onBeforeMount(loadUser)

const handleUpdateUserInformation = async () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        disabledRef.value = false;
        let day = new Date(userForm.value.dob).getDate();
        let month = new Date(userForm.value.dob).getMonth() + 1;
        let year = new Date(userForm.value.dob).getFullYear();
        console.log(userForm.value.dob);
        let dobUser = year + '-' + month + '-' + day;
        loadingBar.finish();
        disabledRef.value = true;
        message.success('Cập nhập thông tin thành công!');
      } catch (err) {
        console.log(err);
        if (!!err.response) {
          message.error(err.response.data.message);
        } else {
          message.error(err.message);
        }
      }
    }
  });
};
const handleChangePassword = async () => {
  formRefAccount.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        disabledRef.value = false;
      } catch (err) {
        console.log(err);
        if (!!err.response) {
          message.error(err.response.data.message);
        } else {
          message.error(err.message);
        }
      }
    }
  });
};
const handleDeleteAccount = async () => {
  try {
    dialog.warning({
      title: 'Xóa tài khoản',
      content: 'Bạn có chắc chắn muốn xóa tài khoản này?',
      positiveText: 'Đồng ý',
      negativeText: 'Hủy',
      onNegativeClick: async () => {
        loadingBar.start();
        disabledRef.value = false;
        loadingBar.finish();
        disabledRef.value = false;
        message.success('Xóa tài khoản thành công!');
        auth.clear();
        router.push('/');
      },
      onNegativeClick: () => {}
    });
  } catch (err) {
    console.log(err);
    if (!!err.response) {
      message.error(err.response.data.message);
    } else {
      message.error(err.message);
    }
  }
};

</script>
<template>
  <div class="container user-infor">
    <div class="wide">
      <h1 class="title">Thông tin cá nhân</h1>
      <div class="infor">
        <div class="infor-basic">
          <div class="avatar">
            <img src="https://cdn-icons-png.flaticon.com/512/9131/9131529.png" alt="avatar" />
          </div>
          <div class="user-bio">
            <h2>
              {{ user.fullName ? user.fullName : 'Không xác định' }}
            </h2>
            <p>
              <IconPhone />
              <span>
                {{ user.phoneNumber ? user.phoneNumber : 'Không xác định' }}
              </span>
            </p>
          </div>
        </div>
        <div class="infor-detail">
          <div class="user-profile" id="user-profile">
            <h3>Thông tin chi tiết</h3>
            <hr />
            <br />
            <n-form :label-width="80" :model="userForm" :rules="rules" ref="formRef" size="large">
              <n-form-item label="Họ tên:" path="fullName" style="margin-bottom: 8px">
                <n-input v-model:value="userForm.fullName" placeholder="" type="text" class="form-input" />
              </n-form-item>
              <n-form-item label="Ngày sinh:" path="dob" style="margin-bottom: 8px">
                <n-date-picker
                  style="width: 100%"
                  v-model:value="userForm.dob"
                  placeholder="2003-10-27"
                  type="date"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item label="Giới tính" path="userForm.gender">
                <n-checkbox-group v-model:value="userForm.gender">
                  <n-space>
                    <n-radio value="Male" class="form-radio"> Nam </n-radio>
                    <n-radio value="Female" class="form-radio"> Nữ </n-radio>
                  </n-space>
                </n-checkbox-group>
              </n-form-item>
              <n-form-item label="Giới thiệu" path="introduction" style="margin-bottom: 8px">
                <n-input
                  v-model:value="userForm.introduction"
                  placeholder=""
                  type="textarea"
                  class="form-input"
                  :autosize="{ minRows: 1, maxRows: 3 }"
                />
              </n-form-item>
              <n-form-item label="Địa chỉ" path="address">
                <n-input
                  :value="userForm.address"
                  type="textarea"
                  placeholder=""
                  class="form-input"
                  :autosize="{ minRows: 1, maxRows: 3 }"
                />
              </n-form-item>
              <n-form-item style="display: flex; justify-content: center">
                <div class="btn-active">
                  <HfButton @click="handleUpdateUserInformation"> Cập nhập thông tin </HfButton>
                </div>
              </n-form-item>
            </n-form>
          </div>
          <div class="user-account" id="user-account">
            <h3>Thông tin tài khoản</h3>
            <hr />
            <br />
            <n-form
              :label-width="80"
              :model="account"
              :rules="rulesAccount"
              ref="formRefAccount"
              size="large"
            >
              <n-form-item label="Tên đăng nhập:" path="username" style="margin-bottom: 8px">
                <n-input readonly v-model:value="account.username" placeholder="" type="text" class="form-input" />
              </n-form-item>
              <n-form-item label="Mật khẩu cũ:" path="oldPassword" style="margin-bottom: 8px">
                <n-input
                  v-model:value="account.oldPassword"
                  placeholder="Chú ý nhập đúng mật khẩu mới thay được"
                  type="text"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item label="Mật khẩu mới:" path="password" style="margin-bottom: 8px">
                <n-input v-model:value="account.password" placeholder="" type="text" class="form-input"/>
              </n-form-item>
              <n-form-item
                label="Nhập lại mật khẩu mới:"
                path="repeatPassword"
                style="margin-bottom: 8px"
              >
                <n-input v-model:value="account.repeatPassword" placeholder="" type="text" class="form-input"/>
              </n-form-item>
              <n-form-item style="display: flex; justify-content: center; margin-top: 12px">
                <HfButton @click="handleChangePassword">Thay đổi mật khẩu</HfButton>
              </n-form-item>
            </n-form>
          </div>
          <HfButton id="user-delete" @click="handleDeleteAccount" class="btn-delete"
            >Xóa tài khoản</HfButton
          >
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped src="./userInfor.scss"></style>

<route lang="yaml">
name: UserInfor
meta:
  layout: default
  <!-- requiresAuth: true -->
</route>
