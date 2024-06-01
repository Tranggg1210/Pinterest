<script setup>
import { changeAvatar, changeInforUser, changeInforUserById, deleteUser, getAllUser } from '@/api/user.api';
import { NButton, useDialog, useLoadingBar, useMessage } from 'naive-ui';
import { h, onBeforeMount } from 'vue';
import moment from 'moment';
import avatar from '@/assets/images/user.png'
import { validateDOB, validateFirstName, validateLastName } from '@/utils/validator';

const users = ref([]);
const loading = ref(false);
const loadingBar = useLoadingBar();
const pagination = ref({ pageSize: 4 });
const message = useMessage();
const showModal = ref(false);
const dialog = useDialog();
const userValue = reactive({
  firstName: null,
  lastName: null,
  birthday: null,
  introduction: null,
  gender: null,
  avatarUrl: null,
  country: null
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
  birthday: {
    required: true,
    validator: validateDOB,
    trigger: ['input', 'blur']
  }
};

const loadUsers = async() => {
  try {
    const result = await getAllUser();
    users.value = result;
  } catch (error) {
    console.log(error);
  }
}
onBeforeMount(async() => {
  try {
    loading.value = true;
    await loadUsers();
    loading.value = false;
    message.success("Tải danh sách người dùng thành công");
  } catch (error) {
    loading.value = false;
    message.error('Tải danh sách người dùng thất bại')
  }

});
const handleDeleteUser = async (id) => {
  try {
    loadingBar.start();
    await deleteUser(id);
    message.success('Xóa người dùng thành công!!!');
    await loadUsers();
  } catch (error) {
    message.error('Xóa người dùng thất bại');
  } finally {
    loadingBar.finish();
  }
};

const beforeUpload = async (data) => {
  try {
    loadingBar.start();
    if (
      data.file.file?.type === 'image/png' ||
      data.file.file?.type === 'image/jpg' ||
      data.file.file?.type === 'image/jpeg' ||
      data.file.file?.type === 'image/gif'
    ) {
      userValue.avatarUrl = data.file.file
      loadingBar.finish();
      return true;
    }
    message.error('Vui lòng nhập đúng định dạng ảnh');
    return false;
  } catch (err) {
    console.log(err);
    message.error('Cập nhập ảnh người dùng không thành công!');
  } finally {
    loadingBar.finish();
  }
};

const userIDDelete = ref(-1);
const columns = [
  {
    title: 'ID',
    key: 'id',
    defaultSortOrder: false,
    sorter: {
      compare: (a, b) => a.id - b.id,
    }
  },
  {
    title: 'Ảnh đại diện',
    key: 'avatarUrl',
    align: 'center',
    render(row) {
      return h('img', {
        src: row.avatarUrl || avatar,
        style: {
          width: '70px',
          height: '70px',
          cursor: 'pointer'
        },
        onClick: () => console.log(row)
      });
    }
  },
  {
    title: 'Họ đệm',
    key: 'lastName'
  },
  {
    title: 'Tên',
    key: 'firstName',
  },
  {
    title: 'Ngày sinh',
    key: 'birthday',
    render: (row) => moment(row.birthday).format('YYYY-MM-DD')
  },
  {
    title: 'Giới tính',
    key: 'gender',
    render: (row) => row.gender ? 'Nam' : 'Nữ'
  },
  {
    title: 'Địa chỉ',
    key: 'country',
  },
  {
  title: 'Actions',
  key: 'actions',
  render(row) {
    const buttons = [
        {
          label: 'Chỉnh sửa',
          type: 'info',
          onClick: () => {
            userValue.firstName = row.firstName;
            userValue.lastName = row.lastName;
            userValue.birthday = new Date(row.birthday).getTime();
            userValue.introduction = row.introduction;
            userValue.gender = row.gender ? 'Male'.toString() : 'Female';
            userValue.country = row.country;
            userIDDelete.value = row.id;
            showModal.value = true;
          }
        },
        {
          label: 'Xóa',
          type: 'primary',
          onClick: () => {
            dialog.warning({
              title: 'Xóa người dùng',
              content: 'Bạn có chắc chắn muốn xóa người dùng này?',
              positiveText: 'Hủy',
              negativeText: 'Xóa người dùng',
              onNegativeClick:  () => {
                  handleDeleteUser(row.id);
              },
              onPositiveClick: () => {}
            });
          }
        }
      ];

      return h(
        'div',
        {
          style: {
            display: 'flex',
            gap: '8px'
          }
        },
        buttons.map((btn) =>
          h(
            NButton,
            {
              strong: true,
              tertiary: true,
              size: 'small',
              type: btn.type,
              onClick: btn.onClick
            },
            { default: () => btn.label }
          )
        )
      );
    }
  }
]
const handleUpdateUserInformation = async () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        let day = new Date(userValue.birthday).getDate();
        let month = new Date(userValue.birthday).getMonth() + 1;
        let year = new Date(userValue.birthday).getFullYear();
        let dobUser = year + '-' + month + '-' + day;
        const newUser = {
          firstName: userValue.firstName,
          lastName: userValue.lastName,
          introduction: userValue.introduction,
          birthday: dobUser,
          gender: userValue.gender === 'Male' ? true : false,
          country: userValue.country,
          avatarUrl: userValue.avatarUrl
        };
        await changeInforUserById(userIDDelete.value,newUser);
        loadingBar.finish();
        message.success('Cập nhập thông tin thành công!');
        showModal.value = false;
        await loadUsers();
      } catch (err) {
        console.log(err);
        message.error('Cập nhập thông tin người dùng thất bại!');
      } finally {
        loadingBar.finish();
      }
    }
  });
};

</script>
<template>
  <div class="admin-users">
    <div class="admin-header">
      <h2>Danh sách người dùng</h2>
    </div>
     <HfLoading v-if="loading"></HfLoading>
    <div v-else>
      <n-data-table
        class="data-table"
        ref="dataTableInst"
        :columns="columns"
        :data="toRaw(users)"
        :pagination="pagination"
      />
    </div>
    <n-modal
      v-model:show="showModal"
      class="custom-card"
      preset="card"
      title="Chỉnh sửa thông tin người dùng"
      style="width: 60%"
      :bordered="false"
    >
      <n-form :label-width="80" :model="userValue" :rules="rules" ref="formRef" size="large">
        <n-form-item label="Họ đệm:" path="lastName" style="margin-bottom: 8px">
          <n-input
            v-model:value="userValue.lastName"
            placeholder=""
            type="text"
            class="input"
          />
        </n-form-item>
        <n-form-item label="Tên:" path="firstName" style="margin-bottom: 8px">
          <n-input
            v-model:value="userValue.firstName"
            placeholder=""
            type="text"
            class="input"
          />
        </n-form-item>
        <n-form-item label="Ngày sinh:" path="birthday" style="margin-bottom: 8px">
          <n-date-picker
            style="width: 100%"
            v-model:value="userValue.birthday"
            placeholder="2003-10-27"
            type="date"
            class="input"
          />
        </n-form-item>
        <n-form-item label="Giới tính:" path="gender">
          <n-radio-group v-model:value="userValue.gender" name="gender">
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
            v-model:value="userValue.introduction"
            placeholder=""
            type="textarea"
            class="input"
            :autosize="{ minRows: 1, maxRows: 3 }"
          />
        </n-form-item>
        <n-form-item label="Địa chỉ:" path="country">
          <n-input
            v-model:value="userValue.country"
            type="textarea"
            placeholder=""
            class="input"
            :autosize="{ minRows: 1, maxRows: 3 }"
          />
        </n-form-item>
        <n-form-item class="container-end">
          <n-button
            @click="() => {
              showModal = false;
            }"
          >
            Hủy
          </n-button>
          <n-button type="success" style="color: white; margin-left: 12px" @click="handleUpdateUserInformation" >
            Chỉnh sửa
          </n-button>
        </n-form-item>
      </n-form>
    </n-modal>
  </div>
</template>


<style lang="scss" scoped>
.admin-users
{
  overflow-x: scroll;
  padding: 20px 40px 40px;
}
.admin-header{
  @include flex(space-between, center);
  margin: 20px 0;
  h2{
    font-weight: 500;
    text-decoration: underline;
  }
}
.admin-users::-webkit-scrollbar {
  height: 8px; 
  display: none;
}
.admin-users::-webkit-scrollbar-thumb {
  background-color: #ccc; 
  border-radius: 4px; 
}
.admin-users::-webkit-scrollbar-track {
  background-color: #f1f1f1;
}
</style>
<route lang="yaml">
name: Admin-Users
meta:
  layout: admin
  requiresAuth: true
</route>
