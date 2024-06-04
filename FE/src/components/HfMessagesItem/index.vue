<script setup>
import { useCurrentUserStore } from '@/stores/currentUser';
import { defineProps } from 'vue';
const {messageData, user} = defineProps(['messageData', 'user']);
const currentUser = useCurrentUserStore();
console.log(user);
const isSend = ref(currentUser.currentUser.userId === Number(messageData.SenderId));
</script>
<template>
   <div>
        <div class="mes-container send" v-if="isSend">
            <div class="message message-send">
                {{ messageData?.Content }}
            </div>
            <div class="user">
                <div class="user-avatar">
                    <img src="@/assets/images/user-avatar.png" alt="avatar" v-if="!currentUser.currentUser.avatar" />
                    <img :src="currentUser.currentUser.avatar" alt="avatar" class="user-avatar" v-else />
                </div>
            </div>
        </div>
        <div class="mes-container" v-else>
            <div class="user">
                <div class="user-avatar">
                    <img src="@/assets/images/user-avatar.png" alt="avatar" v-if="!user?.avatarUrl" />
                    <img :src="user?.avatarUrl" alt="avatar" class="user-avatar" v-else />
                </div>
            </div>
            <div class="message message-send">
                {{ messageData?.Content }}
            </div>
        </div>
   </div>
</template>

<style lang="scss" scoped>
.user-avatar {
    width: 32px;
    height: 32px;
    border-radius: 50%;
    overflow: hidden;
    margin-right: 8px;
    > img {
        width: 100%;
    }
}
.message{
    max-width: 600px;
    background-color: #636363;
    color: #fff;
    padding: 4px 12px;
    margin-top: 12px;
    border-radius: 0 12px 12px 12px;
    word-wrap: break-word;
     &::selection{
        background-color: orange;
    }
}
.mes-container{
    @include flex(left, center);
    width: 100%;
    margin: 20px 0;
}
.send{
    @include flex(end, start);
    .message{
        border-radius: 12px 0px 12px 12px;
        margin-right: 4px;
        margin-top: 12px;
        background-color: rgb(46, 117, 247);
    }
}
</style>
