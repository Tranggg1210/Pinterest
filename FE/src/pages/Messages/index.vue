<script setup>

</script>
<template>
    <div class="messages">
        <div class="messages-content">
            <IconMailOpened class="icon"/>
            <h1>Hãy mở một đoạn chat của bạn.</h1>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.messages{
    height: 100vh;
    @include flex(center, center);
}
.messages-content{
    @include flex(center, center);
}
.icon{
    width: 40px;
    height: 40px;
    color: red;
    margin-right: 12px;
}
</style>
<route lang="yaml">
name: Messages
meta:
    layout: empty
    requiresAuth: true
</route>
