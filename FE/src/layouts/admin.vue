<template>
    <div class="admin">
        <HfSideBar class="admin-sidebar"/>
        <div class="admin-right">
            <HfAdminHeader/>
            <RouterView></RouterView>
        </div>
    </div>
</template>
<style scoped lang="scss">
.admin{
    display: flex;
    justify-content: center;
    align-items: center;
    &-sidebar{
        width: 20%;
    }
    &-right{
        flex-grow: 1;
    }
}
</style>
  
