<script setup>
import { getNotication } from '@/api/notification.api';
import { NButton, useDialog, useLoadingBar, useMessage } from 'naive-ui';
import { h, onBeforeMount } from 'vue';
import moment from 'moment';

const notifications = ref([]);
const loading = ref(false);
const loadingBar = useLoadingBar();
const pagination = ref({ pageSize: 4 });
const message = useMessage();
const dialog = useDialog();
const loadNotifications = async() => {
    try {
        const result = await getNotication();
        notifications.value = result;
        console.log(result);
    } catch (error) {
        console.log(error);
    }
}
onBeforeMount(async() => {
    try {
    loading.value = true;
    await loadNotifications();
    loading.value = false;
    message.success("Tải danh sách thông báo thành công");
  } catch (error) {
    loading.value = false;
    message.error('Tải danh sách thông báo thất bại')
  }
});
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
    title: 'Mô tả',
    key: 'data'
  },
  {
    title: 'Ngày tạo',
    key: 'createdAt',
    render: (text) => moment(text).format('YYYY-MM-DD')
  },
  {
  title: 'Actions',
  key: 'actions',
  render(row) {
    return h(
        NButton,
        {
          strong: true,
          tertiary: true,
          type: 'primary',
          size: 'small',
          onClick: () => play(row)
        },
        { default: () => "Xóa" }
      )
    }
  }
]
</script>

<template>
    <div class="admin-notifications">
    <div class="admin-header">
      <h2>Danh sách thông báo</h2>
    </div>
     <HfLoading v-if="loading"></HfLoading>
    <div v-else>
      <n-data-table
        class="data-table"
        ref="dataTableInst"
        :columns="columns"
        :data="toRaw(notifications)"
        :pagination="pagination"
      />
    </div>
    <!-- <n-modal
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
    </n-modal> -->
  </div>
</template>

<style lang="scss" scoped>
.admin-notifications
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
.admin-notifications::-webkit-scrollbar {
  height: 8px; 
  display: none;
}
.admin-notifications::-webkit-scrollbar-thumb {
  background-color: #ccc; 
  border-radius: 4px; 
}
.admin-notifications::-webkit-scrollbar-track {
  background-color: #f1f1f1;
}
</style>
<route lang="yaml">
name: Admin-Notifications
meta:
    layout: admin
    requiresAuth: true
</route>
