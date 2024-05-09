<script setup>
import { onBeforeMount, reactive } from 'vue';
import { validateDOB, validateFirstName, validateLastName, validatePassword } from '@/utils/validator';
import router from '@/router';
import { useAuthStore } from '@/stores/auth';
import { changeAvatar, changeInforUser, changePassword, deleteUser, getCurrentUser } from '@/api/user.api';

const message = useMessage();
const auth = useAuthStore();
const loadingBar = useLoadingBar();
const disabledRef = ref(true);
const dialog = useDialog();
const userForm = ref({});
const user = ref({});
const formRef = ref(null);
const formRefAccount = ref(null);
const account = reactive({
  oldPassword: '',
  newPassword: '',
  comfirmPassword: ''
});
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
  birthday: {
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
  newPassword: {
    required: true,
    validator: validatePassword,
    trigger: 'blur'
  },
  comfirmPassword: {
    required: true,
    validator: (_, comfirmPassword) => {
      if (comfirmPassword !== account.newPassword) {
        return new Error('Mật khẩu không giống với mật khẩu đã nhập lại!');
      }
      return true;
    },
    trigger: ['blur', 'password-input']
  }
};
const loadUser = async () => {
  try {
    const result = await getCurrentUser();
    user.value = {...result};
    userForm.value = result;
    userForm.value.birthday = new Date(userForm.value.birthday).getTime();
    userForm.value.gender = userForm.value.gender ? 'Male'.toString() : 'Female';
  } catch (err) {
    console.log(err);
    message.error("Lấy thông tin người dùng thất bại");
  }
};

onBeforeMount(loadUser);


const handleFullName = (firstName, lastName) => {
  const fullName = `${lastName} ${firstName} `;
  const formattedFullName = fullName
    .replace(/\s+/g, ' ')
    .trim()
    .replace(/(^|\s)\S/g, (match) => match.toUpperCase());

  return formattedFullName;
};

const beforeUpload = async(data) => {
  try {
    loadingBar.start();
    disabledRef.value = false;
    if (
      data.file.file?.type === 'image/png' ||
      data.file.file?.type === 'image/jpg' ||
      data.file.file?.type === 'image/jpeg' ||
      data.file.file?.type === 'image/gif'
    ) {
      await changeAvatar(data.file);
      await loadUser();
      loadingBar.finish();
      disabledRef.value = true;
      return true;
    }
    message.error('Vui lòng nhập đúng định dạng ảnh');
    return false;
  } catch (err) {
    console.log(err);
    message.error("Cập nhập ảnh người dùng không thành công!");
  }finally{
    loadingBar.finish();
    disabledRef.value = true;
  }
};

const handleUpdateUserInformation = async () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        disabledRef.value = false;
        let day = new Date(userForm.value.birthday).getDate();
        let month = new Date(userForm.value.birthday).getMonth() + 1;
        let year = new Date(userForm.value.birthday).getFullYear();
        let dobUser = year + '-' + month + '-' + day;
        const newUser = {
          firstName: userForm.value.firstName,
          lastName: userForm.value.lastName,
          introduction: userForm.value.introduction,
          birthday: dobUser,
          gender: userForm.value.gender === "Male" ? true : false,
          country: userForm.value.country,
        }
        await changeInforUser(newUser);
        loadingBar.finish();
        disabledRef.value = true;
        message.success('Cập nhập thông tin thành công!');
        await loadUser();
      } catch (err) {
        console.log(err);
        message.error('Cập nhập thông tin người dùng thất bại!');
      }finally{
        loadingBar.finish();
        disabledRef.value = true;
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
        await changePassword(account);
        loadingBar.finish();
        disabledRef.value = true;
        message.success('Thay đổi mật khẩu thành công!');
      } catch (err) {
        console.log(err);
        message.error("Thay đổi mật khẩu không thành công, vui lòng kiểm tra mật khẩu cũ!")
      }finally{
        loadingBar.finish();
        disabledRef.value = true;
      }
    }
  });
};
</script>
<template>
  <div class="container user-infor">
    <div class="wide">
      <h1 class="title">Thông tin cá nhân</h1>
      <div class="infor">
        <div class="infor-basic">
          <div class="avatar">
            <img
              :src="
                user.avatarUrl
                  ? user.avatarUrl
                  : 'https://cdn-icons-png.flaticon.com/512/9131/9131529.png'
              "
              alt="avatar"
            />
          </div>
          <div class="user-bio">
            <h2>
              {{
                user.firstName ? handleFullName(user.firstName, user.lastName) : 'Không xác định'
              }}
            </h2>
            <p>
              <IconMail />
              <span>
                {{ user.email ? user.email : 'Không xác định' }}
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
              <n-form-item label="Họ đệm:" path="lastName" style="margin-bottom: 8px">
                <n-input
                  v-model:value="userForm.lastName"
                  placeholder=""
                  type="text"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item label="Tên:" path="firstName" style="margin-bottom: 8px">
                <n-input
                  v-model:value="userForm.firstName"
                  placeholder=""
                  type="text"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item label="Ngày sinh:" path="birthday" style="margin-bottom: 8px">
                <n-date-picker
                  style="width: 100%"
                  v-model:value="userForm.birthday"
                  placeholder="2003-10-27"
                  type="date"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item label="Giới tính:" path="gender">
                <n-radio-group v-model:value="userForm.gender" name="gender">
                  <n-radio value="Male"> Nam </n-radio>
                  <n-radio value="Female"> Nữ </n-radio>
                </n-radio-group>
              </n-form-item>
              <n-form-item label="Ảnh đại diện:" path="avatarUrl">
                <n-upload @before-upload="beforeUpload">
                  <n-button>Upload ảnh</n-button>
                </n-upload>
              </n-form-item>
              <n-form-item label="Giới thiệu:" path="introduction" style="margin-bottom: 8px">
                <n-input
                  v-model:value="userForm.introduction"
                  placeholder=""
                  type="textarea"
                  class="form-input"
                  :autosize="{ minRows: 1, maxRows: 3 }"
                />
              </n-form-item>
              <n-form-item label="Địa chỉ:" path="country">
                <n-input
                  v-model:value="userForm.country"
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
              <n-form-item label="Tên đăng nhập:" path="userName" style="margin-bottom: 8px">
                <n-input
                  disabled
                  v-model:value="userForm.email"
                  placeholder=""
                  type="text"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item label="Mật khẩu cũ:" path="oldPassword" style="margin-bottom: 8px">
                <n-input
                  v-model:value="account.oldPassword"
                  placeholder="Chú ý nhập đúng mật khẩu mới thay được"
                  type="text"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item label="Mật khẩu mới:" path="newPassword" style="margin-bottom: 8px">
                <n-input
                  v-model:value="account.newPassword"
                  placeholder=""
                  type="text"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item
                label="Nhập lại mật khẩu mới:"
                path="comfirmPassword"
                style="margin-bottom: 8px"
              >
                <n-input
                  v-model:value="account.comfirmPassword"
                  placeholder=""
                  type="text"
                  class="form-input"
                />
              </n-form-item>
              <n-form-item style="display: flex; justify-content: center; margin-top: 12px">
                <HfButton @click="handleChangePassword">Thay đổi mật khẩu</HfButton>
              </n-form-item>
            </n-form>
          </div>
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
  requiresAuth: true
</route>
