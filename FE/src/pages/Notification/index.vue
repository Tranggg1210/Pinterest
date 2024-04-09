<template>
    <!-- start thong bao -->
    <div class="notification">
      <h2 class="noti__title">Cập nhật</h2>
      <div v-for="info in infos" :key="info.id" class="notification__infor">
        <div class="notification__infor__content">
          <div class="notification__infor__content__text">
            <div class="notification__desc">
              <p>{{ info.description }}</p>
            </div>
            <div class="notification__time">
              <p>{{ info.time }}</p>
            </div>
          </div>
          <button class="notification__icon">
            <i class="fa-solid fa-ellipsis"></i>
          </button>
        </div>
        <div class="notification__infor__pic">
          <div v-for="(pic, index) in info.pics" :key="index" class="noti__pic">
            <img :src="pic" alt="">
          </div>
        </div>
      </div>
    </div>
    <!-- end thong bao -->
  </template>
  
  <script>
  export default {
    data() {
      return {
        infos: [
          {
            id: 1,
            description: 'Ghim lấy cảm hứng từ bạn',
            time: '3 tuần',
            pics: [
              './src/img/5b7f27600f485b81f1699d1a05e9e4c1.jpg',
              './src/img/pic_2.jpg',
              './src/img/pic_3.jpg'
            ]
          },
          // Thêm các thông tin khác tương tự
        ]
      };
    }
  };
  </script>
  
  <style lang="scss" scoped>
  .notification {
    margin-top: 80px;
    min-width: 360px;
    max-width: 375px;
    box-shadow: rgba(0, 0, 0, 0.1) -3px 4px 14px 0px;
    overscroll-behavior: none;
    top: 0px;
    transition: top 100ms linear 0s;
    height: calc(-88px + 100vh);
    overflow: scroll;
    border-radius: 15px;
    display: flex;
    flex-direction: column;
    align-items: center;
  
    .noti__title {
      text-align: center;
      font-size: 16px;
      padding: 23px 0;
    }
  
    .notification__infor {
      padding: 5px;
      margin-bottom: 5px;
  
      &:hover {
        background: rgb(233,233,233);
        border-radius: 15px;
      }
  
      &:hover .notification__icon {
        display:inline-block;
      }
  
      .notification__infor__content {
        display: flex;
        align-items: center;
        margin-bottom: 5px;
        justify-content: space-between;
  
        .notification__desc {
          color: #111111;
          margin-right: 3px;
        }
  
        .notification__time {
          color: #767676;
        }
  
        .notification__infor__content__text {
          font-size: 16px;
          margin-left: 5px;
          display: flex;
        }
  
        .notification__icon {
          min-height: 28px;
          width: 28px;
          border-radius: 50%;
          margin-right: 3px;
          background: #fff;
          color:black;
          display: flex;
          justify-content: center;
          align-items: center;
          border: none;
          // display: none;
  
          &:hover {
            color: #fff;
            background: black;
          }
        }
      }
  
      .notification__infor__pic {
        display: flex;
        height: 164px;
        width: 328px;
        gap: 2px;
  
        .noti__pic {
          width: 108px;
          height: 164px;
          display: flex;
  
          img {
            width: 100%;
            object-fit: cover;
          }
  
          &:first-child img {
            border-top-left-radius: 15px;
            border-bottom-left-radius: 15px;
          }
  
          &:last-child img {
            border-top-right-radius: 15px;
            border-bottom-right-radius: 15px;
          }
        }
      }
    }
  }
  </style>
  