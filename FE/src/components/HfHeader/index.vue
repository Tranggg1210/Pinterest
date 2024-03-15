<script setup>
const show = ref(false);
</script>
<template>
    <div class="header container">
        <div class="wide">
           <div style="display: flex; justify-content: center; align-items: center;">
                <router-link to="/" class="header-logo">
                    <IconBrandPinterest size="36"/>
                    <h2>PixelPalette</h2>
                </router-link>
                <router-link class="btn-home" to="/" exact-active-class="active">
                    <HfButton> 
                        Trang chủ
                    </HfButton>
                </router-link>
           </div>
            <div class="header-search">
                <IconSearch size="20px"/>
                <input type="text" placeholder="Tìm kiếm...">
            </div>
            <div class="btn-container">
                <HfButton class="btn-login"> 
                        Đăng nhập
                </HfButton>
                <HfButton class="btn-signup"> 
                        Đăng ký
                </HfButton>
            </div>
            <IconMenu2 size="28" class="mobile-menu-icon" @click="show = true"/>
            <n-drawer v-model:show="show" width="70%">
                <n-drawer-content closable>
                    <template #header>
                        <div style="display: flex; align-items: center;">
                            <IconBrandPinterest size="28" style="color: #e60023;"/>
                            Menu
                        </div>
                    </template> 
                    <div class="mobile-menu">
                        <HfButton class="btn-login"> 
                            Đăng nhập
                        </HfButton>
                        <HfButton class="btn-signup"> 
                            Đăng ký
                        </HfButton>
                    </div>
                </n-drawer-content>
            </n-drawer>
        </div>
    </div>
    <div class="cushion"></div>
</template>


<style lang="scss" scoped>
.header{
    background-color: $white-color;
    box-shadow: 1px 1px 20px rgba(0 0 0/2%);
    padding: 16px 0;
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
}
.header>.wide{
    @include flex(space-between, center)
}
.header-logo{
    @include flex(center, center);
    color: $primary-color;
    margin-right: 6px;
    svg{
        margin-right: 2px !important;
    }

    @include mobile{
        h2{
            display: none;
        }
    }
    @include small-tablet{
        h2{
            font-size: 18px;
        }
    }
}

.active button{
    background-color: $black-color;
    color: $white-color;
}
.btn-home{
    @include mobile{
       display: none;
    }
    @include small-tablet{
        display: none;
    }
    @include tablet{
        display: none;
    }
}
.header-search{
    width: 60%;
    border: none;
    background-color: $secondary-color;
    padding: 8px 16px;
    @include flex(center, center);
    border-radius: 32px;

    input{
        width: 98%;
        border: none;
        outline: none;
        font-size: 16px;
        padding: 3px 4px;
        background-color: transparent;
    }
    @include tablet {
        width: 50%;
    }
}
.btn-container{
    @include flex(center,center);
    .btn-signup{
        background-color: $primary-color;
        color: $white-color;
        margin: 0;
    } 
    @include mobile{
        display: none;
    }
    @include small-tablet{
        display: none;
    }
}
.mobile-menu-icon{
    display: none;
    cursor: pointer;
    transition: all 0.4s linear;

    &:hover{
        color: $primary-color;
    }
    @include mobile{
        display: block;
    }
    @include small-tablet{
        display: block;
    }
}
.mobile-menu{
    button{
        border-radius: 0;
        background-color: transparent;
        margin-left: -6px;
        width: 106%;
        text-align: left;
        @include small-tablet{
            width: 103%;
        }
    }
}
.cushion{
    height: 76px;
}
</style>