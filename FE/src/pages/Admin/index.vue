<script setup>
import { useRouter } from 'vue-router';

const user = ref({});
const router = useRouter();
const options = [
    {
        id: 1,
        title: "Số lượt truy cập trong ngày",
        quality: 0,
        desc: 'lượt truy cập',
    },
    {
        id: 2,
        title: "Số lượng người dùng",
        quality: 0,
        desc: 'người dùng',
    },
    {
        id: 3,
        title: "Số lượng bài viết",
        quality: 0,
        desc: 'bài viết',
    },
    {
        id: 4,
        title: "Số lượng thông báo",
        quality: 0,
        desc: 'thông báo',
    }
    
]
const handleFullName = (firstName, lastName) => {
  const fullName = `${lastName} ${firstName} `;
  const formattedFullName = fullName
    .replace(/\s+/g, ' ')
    .trim()
    .replace(/(^|\s)\S/g, (match) => match.toUpperCase());

  return formattedFullName;
};
const goToPage = () => {
    router.push("/user-infor")
}
</script>
<template>
    <div class="home-admin">
        <div class="home-banner container">
            <div class="basic-user">
                <div class="user-background">
                    <img
                    :src="user?.avatarUrl"
                    alt="background"
                    v-if="user?.avatarUrl"
                    />
                    <img
                    v-else
                    src="@/assets/images/user-avatar.png"
                    alt="background"
                    style="width: 72%;"
                    />
                </div>
                <h1 class="user-name">
                    {{ user?.firstName ? handleFullName(user?.firstName, user?.lastName) : "Chưa xác định" }}
                </h1>
                <n-button type="warning" @click="goToPage">Chỉnh sửa profile</n-button>
            </div>
        </div>

        <div class="card-quality">
            <div>
                <HfCardHomeAdmin v-for="item in options" :class="`collection-item-${item.id}`" :key="item.id" :data="item"/>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.home-banner{
    height: 44vh;
    background-image:linear-gradient(to bottom right, #b2b6ff, #7BD5F5);
    padding: 80px 0;
}
.basic-user {
   @include flex(center, center);
   flex-direction: column;
   align-items: center;
  .user-background {
    width: 110px;
    height: 110px;
    border-radius: 50%;
    border: 4px solid #fff;
    overflow: hidden;
    cursor: pointer;
    transition: all 0.3s linear;
    > img {
        width: 100% !important;
    }
    &:hover {
      scale: 0.9;
    }
  }
  .user-name {
    margin: 16px 0 0;
    font-weight: 500;
    color: #fff;
    margin-bottom: 20px;
  }
}
.card-quality{
    @include flex(center, center);
    margin: 40px 0;
   >div{
        width: 80%;
        @include flex(center, center);
        flex-wrap: wrap;
        gap: 20px;
   }
}
.collection-item-1 {
    background-color: #18975F; 
}
.collection-item-2 {
background-color: #C31162; 
}
.collection-item-3 {
background-color: #ffba00;
}
.collection-item-4 {
background-color: #cc3433; 
}
.collection-item-5 {
background-color: #0095db; 
}
.collection-item-6 {
background-color: #814235; 
}
</style>
<route lang="yaml">
    name: Admin
    meta:
      layout: admin
</route>
