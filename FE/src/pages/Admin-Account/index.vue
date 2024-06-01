<script setup>
import { editRoles, getAllAccount } from '@/api/account.api';
import { register } from '@/api/auth.api';
import { validateEmail, validateFirstName, validateLastName, validatePassword } from '@/utils/validator';
import { NButton, NTag, useDialog, useLoadingBar, useMessage } from 'naive-ui';
import { h } from 'vue';

const accountList = ref([]);
const loading = ref(false);
const loadingBar = useLoadingBar();
const pagination = ref({ pageSize: 4 });
const message = useMessage();
const dialog = useDialog();
const showModal = ref(false);
const showModalEdit = ref(false);
const account = reactive({
  firstName: '',
  lastName: '',
  email: '',
  password: ''
});
const roles = ref({
    role: null
});
const formRef = ref(null);
const formRefRoles = ref(null);
const generalOptions = [
    {
        label: 'Member',value: 'Member'
    },
    {
        label: 'Admin',value: 'Admin',
    }
]
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
const rulesEditRoles = {
    role: {
        required: true,
        trigger: ['blur', 'change'],
        message: 'Vui lòng chọn quyền'
    }
}
const loadAccountList = async() => {
    try {
        const result = await getAllAccount();
        accountList.value = result;
    } catch (error) {
        console.log(error);
    }
}
onBeforeMount(async() => {
    try {
    loading.value = true;
    await loadAccountList();
    loading.value = false;
    message.success("Tải danh sách người dùng thành công");
  } catch (error) {
    loading.value = false;
    message.error('Tải danh sách người dùng thất bại')
  }
});
const userIdEditRole = ref(-1);
const handleEditRole = async() => {
    try {
        loadingBar.start();
        if(roles.value.role === 'Member')
        {
            await editRoles(userIdEditRole.value, ['Member']);
        }else{
            await editRoles(userIdEditRole.value, ['Member', 'Admin']);
        }
        await loadAccountList();
        showModalEdit.value = false;
        message.success("Chỉnh sửa quyền của người dùng thành công");
        loadingBar.finish();
    } catch (error) {
        loadingBar.error()
        message.error('Lỗi, không thể chỉnh sửa quyền của người dùng này')
    }finally{
        loadingBar.finish();
    }
}
const handleBlockUser = async(id) => {
  try {
      loadingBar.start();
      await editRoles(id, ['Blocker']);
      await loadAccountList();
      showModalEdit.value = false;
      message.success("Khóa người dùng thành công!");
      loadingBar.finish();
    } catch (error) {
        loadingBar.error()
        message.error('Lỗi, không thể khóa người dùng!')
    }finally{
        loadingBar.finish();
    }
}
const handleOpenBlockUser = async(id) => {
  try {
      loadingBar.start();
      await editRoles(id, ['Member']);
      await loadAccountList();
      showModalEdit.value = false;
      message.success("Mở khóa người dùng thành công!");
      loadingBar.finish();
    } catch (error) {
        loadingBar.error()
        message.error('Lỗi, không thể mở khóa người dùng!')
    }finally{
        loadingBar.finish();
    }
}
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
    title: 'Tên đăng nhập',
    key: 'userName'
  },
  {
    title: 'Vai trò',
    key: 'roles',
    render (row) {
        const tags = row.roles.map((tagKey) => {
          return h(
            NTag,
            {
              style: {
                marginRight: '6px'
              },
              type: 'info',
              bordered: false
            },
            {
              default: () => tagKey
            }
          )
        })
        return tags
    }
  },
  {
  title: 'Actions',
  key: 'actions',
  render(row) {
    const isLocked = row.roles.includes('Blocker');
    const buttons = [
        {
          label: 'Chỉnh sửa quyền',
          type: 'info',
          onClick: () => {
            userIdEditRole.value = row.id;
            if(row.roles.includes('Admin'))
            {
                roles.value.role = 'Admin';
            }else{
                roles.value.role = 'Member';
            }
            showModalEdit.value = true;
          }
        },
        {
          label: isLocked ? 'Mở khóa tài khoản' : 'Khóa tài khoản',
          type: 'primary',
          onClick: () => {
            dialog.warning({
              title: isLocked ? 'Mở khóa tài khoản' : 'Khóa tài khoản',
              content: isLocked ? 'Bạn có chắc chắn muốn mở khóa tài khoản này?' : 'Bạn có chắc chắn muốn khóa tài khoản này?' ,
              positiveText: 'Hủy',
              negativeText: isLocked ? 'Mở khóa tài khoản' : 'Khóa tài khoản',
              onNegativeClick:  () => {
                if(isLocked)
                {
                  handleOpenBlockUser(row.id)
                }else{
                  handleBlockUser(row.id)
                }
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
];
const handleCreateAccount = () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        await register(account);
        await loadAccountList();
        showModal.value = false;
        message.success('Tạo tài khoản thành công');
        loadingBar.finish();
      } catch (err) {
        loadingBar.error();
        message.error('Tạo tài khoản không thành công');
      }finally{
        loadingBar.finish()
      }
    }
  });
};
</script>

<template>
    <div class="admin-account">
        <div class="admin-header">
            <h2>Danh sách tài khoản</h2>
            <n-button type="warning" @click="showModal = true">
                Tạo tài khoản
            </n-button>
        </div>
        <HfLoading v-if="loading"></HfLoading>
        <div v-else>
            <n-data-table
                class="data-table"
                ref="dataTableInst"
                :columns="columns"
                :data="toRaw(accountList)"
                :pagination="pagination"
            />
        </div>
        <n-modal
            v-model:show="showModal"
            class="custom-card"
            preset="card"
            title="Thêm tài khoản"
            style="width: 60%"
            :bordered="false"
            >
            <n-form ref="formRef" :model="account" :rules="rules" size="large">
                <n-form-item path="firstName" label="Tên">
                <n-input v-model:value="account.firstName" placeholder="Tên" class="input" />
                </n-form-item>
                <n-form-item path="lastName" label="Họ">
                <n-input v-model:value="account.lastName" placeholder="Họ" class="input" />
                </n-form-item>
                <n-form-item path="email" label="Email">
                <n-input v-model:value="account.email" placeholder="Email" class="input" />
                </n-form-item>
                <n-form-item path="password" label="Mật khẩu" style="margin-top: 4px">
                <n-input
                    v-model:value="account.password"
                    placeholder="Mật khẩu"
                    type="password"
                    show-password-on="click"
                    class="input"
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
                    <n-button type="success" style="color: white; margin-left: 12px" @click="handleCreateAccount">
                        Tạo tài khoản
                    </n-button>
                </n-form-item>
            </n-form>
        </n-modal>
        <n-modal
            v-model:show="showModalEdit"
            class="custom-card"
            preset="card"
            title="Chỉnh sửa quyền"
            style="width: 60%"
            :bordered="false"
            >
            <n-form
                ref="formRefRoles"
                :model="roles"
                :rules="rulesEditRoles"
                label-placement="top"
            >
            <n-form-item :span="12" label="Quyền:" path="role">
                <n-select
                    v-model:value="roles.role"
                    :options="generalOptions"
                    class="input"
                    :bordered="false"
                />
            </n-form-item>
            <n-form-item class="container-end">
                <n-button
                    @click="() => {
                        showModalEdit = false;
                    }"
                >
                    Hủy
                </n-button>
                <n-button type="success" style="color: white; margin-left: 12px" @click="handleEditRole">
                    Chỉnh sửa
                </n-button>
            </n-form-item>
  </n-form>
            
        </n-modal>
    </div>
</template>


<style lang="scss" scoped>
.admin-account
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
.admin-account::-webkit-scrollbar {
  height: 8px; 
  display: none;
}
.admin-account::-webkit-scrollbar-thumb {
  background-color: #ccc; 
  border-radius: 4px; 
}
.admin-account::-webkit-scrollbar-track {
  background-color: #f1f1f1;
}
</style>
<route lang="yaml">
name: Admin-Account
meta:
    layout: admin
    requiresAuth: true
</route>
  
