<template>
    <div class="login">
        <div class="login__logo">
            <img src="../../assets/images/logo.png" alt="">
        </div>
        <div class="login__title">
            <p class="login__title-heading">Chào mừng bạn đến với Pinterest</p>
        </div>
        <form class="login__wrapper" @submit.prevent="validateForm">
            <div class="login__wrapper-email">
                <label for="email">Email</label> <br>
                <input type="email" id="email" v-model="email" placeholder="Email" required>
            </div>
            <div class="login__wrapper-password">
                <label for="password">Mật khẩu</label> <br>
                <input type="password" id="password" v-model="password" placeholder="Mật khẩu" required>
            </div>
            <div class="login__wrapper-forgetpass">
                <p>Quên mật khẩu?</p>
            </div>
            <button type="submit" class="login__wrapper-button button-login">Đăng nhập</button>
            <div class="login__wrapper_or">
                <p>Hoặc</p>
            </div> 
            <div class="login__wrapper-button button-fb">
                <i class="fa-brands fa-facebook"></i>
                <p>Tiếp tục với Facebook</p>
            </div> 
            <div class="login__wrapper-button button-gg">
                <i class="fa-brands fa-google"></i>
                <p>Tiếp tục truy cập Google</p>
            </div> 
            <div class="login__wrapper-infor">
                <p>Bằng cách tiếp tục, bạn đồng ý với <br>
                <a href="#" class="login__strong-infor" >Điều khoản dịch vụ</a> của Pinterest và xác nhận rằng <br>
                bạn đã đọc <a href="#" class="login__strong-infor">Chính sách quyền riêng tư</a> của chúng tôi. <br>
                <a href="#" class="login__strong-infor">Thông báo khi thu thập.</a>
                </p>
            </div>
            <div class="login__border"></div>
            <div class="login__wrapper-register">
                <p><a href="#" class="login__strong-infor">Chưa tham gia Pinterest? Đăng ký</a> <br>
                Bạn là doanh nghiệp? <a href="#" class="login__strong-infor">Hãy bắt đầu tại đây!</a>
                </p>
            </div>
        </form>
    </div>
</template>

<script>
export default {
    data() {
        return {
            email: '',
            password: ''
        };
    },
    methods: {
        validateForm() {
            if (!this.email || !this.password) {
                alert('Vui lòng điền đầy đủ thông tin.');
                return false;
            }
            if (!this.isValidEmail(this.email)) {
                alert('Vui lòng điền đúng định dạng email.');
                return false;
            }
            if (this.password.length <= 8) {
                alert('Vui lòng nhập mật khẩu dài hơn 8 ký tự.');
                return false;
            }
            // Nếu các điều kiện đều đúng, bạn có thể tiếp tục xử lý đăng nhập ở đây
            console.log('Form is valid. Proceed with login.');
            return true;
        },
        isValidEmail(email) {
            return /\S+@\S+\.\S+/.test(email);
        }
    }
};
</script>
<!-- css -->
<style lang="scss" scoped>
    @import url('https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap');
    
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen-Sans, Ubuntu, Cantarell, "Fira Sans", "Droid Sans", "Helvetica Neue", Helvetica, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "ヒラギノ角ゴ Pro W3", "Hiragino Kaku Gothic Pro", メイリオ, Meiryo, "ＭＳ Ｐゴシック", Arial, sans-serif;
}

html {
    display: flex;
    justify-content: center;
    background: #666666;
}

.login {
    width: 484px;
    /* height: 720px; */
    background: #fff;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 20px 10px 24px 10px;
    border-radius: 35px;

    &__wrapper {
        width: 268px;
    }

    &__logo img {
        width: 39px;
        height: 39px;
        display: block;
        margin: 0 auto;
    }

    &__title {
        width: 400px;
        height: 85px;
    }

    &__title-heading {
        font-size: 32px;
        color: #333333;
        text-align: center;
        font-weight: 600;
    }

    &__wrapper-email {
        label {
            font-size: 14px;
            color: #111111;
            padding-left: 8px;
            display: inline-block;
            margin-bottom: 5px;
        }

        input {
            padding: 12px 16px;
            width: 100%;
            border-radius: 12px;
            border: 2px solid #cdcdcd;
        }
    }

    &__wrapper-password {
        label {
            font-size: 14px;
            color: #111111;
            padding-left: 8px;
            display: inline-block;
            margin: 5px 0;
        }

        input {
            padding: 12px 16px;
            width: 100%;
            border-radius: 12px;
            border: 2px solid #cdcdcd;
        }
    }

    &__wrapper-forgetpass {
        font-size: 14px;
        color: #111111;
        font-weight: 450;
        display: inline-block;
        margin: 5px 0;
    }

    &__wrapper-button {
        margin: 8px 0px 0px;
        padding: 0px 18px;
        text-align: center;
        width: 100%;
        height: 40px;
        border-radius: 999px;

        &.button-login {
            color: #ffffff;
            font-size: 15px;
            background: #e60023;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
        }
    }

    &__wrapper_or {
        p {
            text-align: center;
            font-size: 14px;
            font-weight: bold;
            color: #333333;
            margin: 8px 0;
        }
    }

    &__wrapper-button {
        &.button-fb {
            color: #ffffff;
            font-size: 16px;
            background: #1877f2;
            display: flex;
            align-items: center;
            justify-content: center;
            letter-spacing: 0.1;
            font-weight: bold;
        }

        &.button-gg {
            color: #111111;
            font-size: 15px;
            background: #f6f6f8;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: 500;
            margin: 10px 0 10px 0;
        }

        p {
            width: 88%;
        }
    }

    &__wrapper-infor {
        p {
            text-align: center;
            font-size: 11px;
            color: #767676;
            padding: 10px 0;
        }
    }

    &__strong-infor {
        font-weight: bold;
        text-decoration: none;
        color: #111111;
    }

    &__border {
        border-bottom: 1px solid #e6e6e6;
        margin: 10px auto;
        width: 110px;
    }

    &__wrapper-register {
        font-size: 12px;
        text-align: center;
        color: #333333;
        line-height: 2;

        a:last-child {
            font-weight: 500;
        }
    }
}
</style>
<script>
    function validateForm() {
        var email = document.getElementById('email').value;
        var password = document.getElementById('password').value;

        if (email === '' || password === '') {
            alert('Vui lòng điền đầy đủ thông tin.');
            return false;
        }
        if(email !== "@gmail.com")
        {
            alert('Vui lòng điền đúng tài khoản email.');
            return false;
        }
        if(password.length <=8)
        {
            alert('Vui lòng nhập mật khẩu dài hơn 8 ký tự');
            return false;
        }
        return true;
    }
</script>