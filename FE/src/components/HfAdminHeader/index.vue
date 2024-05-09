<script setup>

</script>
<template>
    <div class="header">
          <RouterLink
            style="
              display: flex;
              justify-content: space-between;
              align-items: center;
              cursor: pointer;
            "
            to="/user-infor"
          >
            <img src="@/assets/images/user-avatar.png" alt="avatar" class="user-avatar" />
            <span>Xin chào, Admin!</span>
          </RouterLink>
    </div>
</template>

<style lang="scss" scoped>
.header{
    padding: 12px 24px;
    box-shadow: 0 0 12px rgba(0 0 0/10%);
    @include flex(end, center);
    img{
        width: 36px;
        height: 36px;
        margin-right: 6px;
        @include mobile{
            width: 28px;
            height: 28px;
        }
    }
    a:hover{
        opacity: 0.6;
    }
}
</style>
