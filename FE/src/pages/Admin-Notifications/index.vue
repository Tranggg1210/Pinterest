<script setup>
import { createNotification, deleteNotification, getNotication } from '@/api/notification.api';
import { NButton, useDialog, useLoadingBar, useMessage } from 'naive-ui';
import { h, onBeforeMount } from 'vue';
import moment from 'moment';

const notifications = ref([]);
const loading = ref(false);
const loadingBar = useLoadingBar();
const pagination = ref({ pageSize: 4 });
const message = useMessage();
const dialog = useDialog();
const showModal = ref(false);
const notificationValue = ref({
  data: null
});
const formRef = ref(null);
const rules = {
  data: {
    required: true,
    validator: (_, data) => {
      if (data === null || typeof data === 'undefined') {
        return new Error('Vui lòng nhập nội dung thông báo!');
      }

      if (data.trim() === '') {
        return new Error('Vui lòng nhập nội dung thông báo!');
      }
    },
    trigger: ['blur', 'input']
  }
};
const loadNotifications = async () => {
  try {
    const result = await getNotication();
    notifications.value = result;
  } catch (error) {
    console.log(error);
  }
};
onBeforeMount(async () => {
  try {
    loading.value = true;
    await loadNotifications();
    loading.value = false;
    message.success('Tải danh sách thông báo thành công');
  } catch (error) {
    loading.value = false;
    message.error('Tải danh sách thông báo thất bại');
  }
});
const handleCreateNotification = async () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        await createNotification({
          data: notificationValue.value.data
        });
        await loadNotifications();
        showModal.value = false;
        message.success('Tạo thông báo thành công!');
        loadingBar.finish();
      } catch (error) {
        console.log(error);
        loadingBar.error();
        message.error('Lỗi, tạo thông báo thất bại');
      } finally {
        loadingBar.finish();
      }
    }
  });
};
const handleDeleteNotificationById = async (id) => {
  try {
    loadingBar.start();
    await deleteNotification(id);
    message.success('Xóa thông báo thành công!');
    await loadNotifications();
    loadingBar.finish();
  } catch (error) {
    console.log(error);
    loadingBar.error();
    message.error('Xóa thông báo thất bại');
  } finally {
    loadingBar.finish();
  }
};
const columns = [
  {
    title: 'ID',
    key: 'id',
    defaultSortOrder: false,
    sorter: {
      compare: (a, b) => a.id - b.id
    }
  },
  {
    title: 'Mô tả',
    key: 'data'
  },
  {
    title: 'Ngày tạo',
    key: 'createdAt',
    render: (row) => moment(row.createdAt).format('YYYY-MM-DD')
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
          onClick: () => {
            dialog.warning({
              title: 'Xóa thông báo',
              content: 'Bạn có chắc chắn muốn xóa thông báo này?',
              positiveText: 'Hủy',
              negativeText: 'Xóa thông báo',
              onNegativeClick: () => {
                handleDeleteNotificationById(row.id);
              },
              onPositiveClick: () => {}
            });
          }
        },
        { default: () => 'Xóa' }
      );
    }
  }
];
</script>

<template>
  <div class="admin-notifications">
    <div class="admin-header">
      <h2>Danh sách thông báo</h2>
      <n-button type="warning" @click="showModal = true"> Thêm thông báo </n-button>
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
    <n-modal
      v-model:show="showModal"
      class="custom-card"
      preset="card"
      title="Tạo thông báo"
      style="width: 60%"
      :bordered="false"
    >
      <n-form
        :label-width="80"
        :model="notificationValue"
        :rules="rules"
        ref="formRef"
        size="large"
      >
        <n-form-item label="Nội dung thông báo:" path="data">
          <n-input
            v-model:value="notificationValue.data"
            type="textarea"
            placeholder=""
            class="input"
            :autosize="{ minRows: 1, maxRows: 6 }"
          />
        </n-form-item>
        <n-form-item class="container-end">
          <n-button
            @click="
              () => {
                showModal = false;
              }
            "
          >
            Hủy
          </n-button>
          <n-button
            type="success"
            style="color: white; margin-left: 12px"
            @click="handleCreateNotification"
          >
            Tạo thông báo
          </n-button>
        </n-form-item>
      </n-form>
    </n-modal>
  </div>
</template>

<style lang="scss" scoped>
.admin-notifications {
  overflow-x: scroll;
  padding: 20px 40px 40px;
}
.admin-header {
  @include flex(space-between, center);
  margin: 20px 0;
  h2 {
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
