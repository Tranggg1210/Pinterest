<template>
  <div class="container">
    <div class="Header">
      <div class="Header__title">
        Chỉnh sửa Ghim
      </div>
      <div class="Header__icon">
        <button class="button__cancel"><i class="fa-solid fa-x"></i></button>
      </div>
    </div>
    <div class="Form">
      <div class="Form__title">
        <label for="">Tiêu đề</label><br>
        <input type="text" placeholder="Thêm tiêu đề">
      </div>
      <div class="Form__desc">
        <label for="">Mô tả</label><br>
        <textarea class="Form__input" placeholder="Viết mô tả của bạn tại đây"></textarea>
      </div>
      <div class="Form__link">
        <label for="">Liên kết</label><br>
        <input type="text" placeholder="Thêm liên kết">
      </div>
      <div class="Form__extra">
        <div class="Form__extra__table">
          <label for="Bảng">Bảng</label>
          <input v-model="Ontable" type="text" placeholder="Không có bảng nào">
        </div>
        <div class="Form__extra__section">
          <label for="Phần">Phần</label>
          <input v-model="Onsection" type="text" placeholder="Không có phần nào">
        </div>
      </div>
      <div class="Form__choice">
        <a href="#">Tùy chọn khác <i class="fa-solid fa-chevron-down"></i></a>
      </div>
    </div>
    <div class="Footer">
      <input type="button" value="Xóa" class="button button__delete">
      <input type="button" value="Lưu" class="button button__save">
    </div>
  </div>
</template>
  
<script>
</script>
  
<style lang="scss" scoped>
  .container {
    position: relative;
    width: 500px;
    background: #f6f6f6;
    position: fixed;
    top: 0;
    right: 0;
  }
  
  .Header {
    padding: 30px;
    height: 100px;
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
  
    &__title {
      font-size: 30px;
    }
  
    &__icon button {
      font-size: 20px;
      border: none;
      background: #f6f6f6;
      margin-right: 20px;
    }
  }
  
  .Form {
    border-top: 1px solid gray;
    border-bottom: 1px solid gray;
    overflow-y: scroll;
    padding: 30px;
    height: 400px;
    display: flex;
    flex-direction: column;
    gap: 35px;
  
    & input {
      margin-top: 10px;
      width: 100%;
      border-radius: 13px;
      padding: 8px;
    }
  
    &__extra {
      display: flex;
      justify-content: space-between;
      align-items: center;
      gap: 20px;
    }
  }
  
  .Form__choice a {
    text-decoration: none;
  }
  
  .Footer {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    height: 100px;
    gap: 10px;
  
    & .button {
      display: inline-block;
      padding: 12px 16px;
      border-radius: 999px;
      border: none;
      font-weight: 500;
      font-size: 15px;
      cursor: pointer;
    }
  
    & .button__delete {
      background: #e9e9e9;
    }
  
    & .button__save {
      background: #e60023;
      color: #fff;
    }
  }
</style>
  