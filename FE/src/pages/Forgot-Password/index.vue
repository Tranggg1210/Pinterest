<script setup>
import { computed, reactive } from 'vue';
import { validateEmail } from '@/utils/validator';
import { useBreakpoints } from '@/composables/useBreakpoints';
const { width, device } = useBreakpoints();
const search = reactive({
    email: ""
});
const formRef = ref(null);
const message = useMessage();
const handleSearch = () => {
  if(search.email.trim() === '')
  {
    message.warning('Vui lòng nhập email của bạn')
  }
}
const isShowBtnForgotPassword = computed(() => validateEmail(search.email));
const handleForgotPassword = async() => {
    console.log("Forgot-Password");
}
</script>

<template>
    <div class="forgot-password container">
        <div class="wide">
            <h1>Chúng ta hãy tìm tài khoản PixelPalette của bạn</h1>
            <p>Email, tên hoặc tên người dùng của bạn là gì?</p>
            <n-form class="form-search" 
                :label-width="80"
                :model="search"
                ref="formRef"
                size="large"
            >
              <n-form-item path="search.email" label="" class="inp-search">
                <n-input v-model:value="search.email" placeholder="Nhập gmail của bạn"  
                  style="padding: 5px; border: 1px solid #f1f1f1; text-align: left; border-radius: 32px;"/>
              </n-form-item>
              <n-form-item>
                <HfButton @click="handleSearch" style="margin-right: 0;" type="submit" v-if="width > 480">Tìm kiếm</HfButton>
                <HfButton @click="handleSearch" style="margin-right: 0;" type="submit" v-else><IconSearch/></HfButton>
              </n-form-item>
            </n-form>
            <HfButton @click="handleForgotPassword" class="btn-forgot-password" v-show="isShowBtnForgotPassword">Gửi lại mật khẩu mới</HfButton>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.forgot-password{
    margin-top: 40px;
    text-align: center;
    h1{
        font-weight: 500;
        @include mobile{
          font-size: 20px;
        }
    }
    .wide{
        width: 40%;
        @include flex(center, center);
        flex-direction: column;
        @include mobile
        {
          width: 90%;
        }
        @include small-tablet{
          width: 90%;
        }
        @include tablet{
          width: 70%;
        }
    }
    p{
      margin:14px 0 0;
    }
    .form-search{
        width: 80%;
        @include flex(space-between, center);
       .inp-search{
          width: 80%;
          @include mobile{
            width: 80%;
          }
        }
        button{
          padding: 14px 18px;
          @include mobile{
            padding: 12px;
            @include flex(center, center);
            margin-left: 0;
          }
        }
      @include mobile()
      {
        width: 100%;
      }
    }
    .btn-forgot-password{
      margin-top: 0px;
      width: 80%;
      padding: 14px 0;
      background-color: $primary-color;
      color: $white-color;
      &:hover{
        background-color: $black-color;
      }
      @include mobile{
        width: 100%;
      }
    }

    @include mobile{
      margin-top: 20px;
    }
}
</style>
<route lang="yaml">
    name: ForgotPassword
    meta:
      layout: default
</route>