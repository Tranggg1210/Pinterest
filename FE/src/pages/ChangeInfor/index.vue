<script setup>
import { NInput } from 'naive-ui'
import { NSelect } from 'naive-ui'
import { NButton } from 'naive-ui'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faChevronDown } from '@fortawesome/free-solid-svg-icons'
import { NInputGroup } from 'naive-ui'


</script>

<template>
  <div class="container">
    <header>
      <h1>Chỉnh sửa Ghim</h1>
      <a href="!#"><i class="fa-solid fa-xmark cancel"></i></a>
    </header>
    <section id="section">
      <label for="Title">Tiêu đề</label>
      <n-input class="input" v-model:value="value" type="text" placeholder="Thêm tiêu đề" />
      <label for="Describe">Mô tả</label>
      <n-input class="input"
      v-model:value="value"
      type="textarea"
      placeholder="Viết mô tả cho Ghim của bạn ở đây hoặc thêm một danh sách cụ thể bên dưới"
      />
      <label for="Link">Liên kết</label>
      <n-input class="input" v-model:value="value" type="text" placeholder="Thêm liên kết" />
      <label for="table">Bảng</label>
      <n-select class="input select" :style="{ width: '50%'}" :options="selectOptions" placeholder="Chọn bảng"/>    
      <p class="other">Tùy chọn khác <i class="fa-solid fa-chevron-down"></i></p> 
  </section>
  <hr>
    <footer>
      <div class="button">
      <n-button  class="buttonn" strong secondary>
      Xóa
      </n-button>
      <n-button class="buttonn" strong secondary type="error">
      Lưu
      </n-button>
      </div>
    </footer>
  </div>
</template>

<style scoped >
*{
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}
.container{
  width: 550px;
  height: 100vh;
  /* background: #f3f3f3; */
  border: 1px solid black;
}
header{
  height: 100px;
  padding: 24px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  .cancel{
    font-size: 30px;
    margin-right: 15px;
    color: black;
  }
  h1{
    font-weight: 600;
  }
}
section{
  padding: 24px;
  overflow-y: scroll;
  height: 400px;
  border-top: 1px solid black;
  label:last-child{
    font-size: ;
  }
  .input{
    padding: 8px 12px;
    border-radius: 15px;
    margin-bottom: 12px;
    border: 2px solid gray;
  }
  .input:focus-within {
    border: 3px solid #007aff;
  }
  .select {
    margin-bottom: 22px;
  }
  .other{
    font-size: 16px;
    font-weight: 500;
  }
}
footer{
  height: calc(100vh - 110px - 400px);
  display: flex;
  justify-content: flex-end;
  margin-right: 40px;
  align-items: center;
  button{
  padding: 20px;
  border-radius: 30px;
  margin-left: 10px;
  }
  .buttonn{
    font-weight: 600;
  }
}
</style>
