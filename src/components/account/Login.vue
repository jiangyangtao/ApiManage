<template>
  <div class="limiter">
    <div class="container-login100">
      <div class="wrap-login100 p-l-55 p-r-55 p-t-65 p-b-54">
        <form class="login100-form validate-form">
          <span class="login100-form-title p-b-49">登录</span>
          <div class="wrap-input100 validate-input m-b-23" data-validate="请输入用户名">
            <span class="label-input100">用户名</span>
            <input class="input100" type="text" name="username" placeholder="请输入用户名" autocomplete="off">
            <span class="focus-input100" data-symbol=""></span>
          </div>
          <div class="wrap-input100 validate-input" data-validate="请输入密码">
            <span class="label-input100">密码</span>
            <input class="input100" type="password" name="pass" placeholder="请输入密码">
            <span class="focus-input100" data-symbol=""></span>
          </div>
          <div class="text-right p-t-8 p-b-31">
            <a href="javascript:">忘记密码？</a>
          </div>
          <div class="container-login100-form-btn">
            <div class="wrap-login100-form-btn">
            <div class="login100-form-bgbtn"></div>
              <button class="login100-form-btn">登 录</button>
            </div>
          </div>
          <div class="txt1 text-center p-t-54 p-b-20">

          </div>
          <div class="flex-col-c p-t-25">
            <a href="javascript:" class="txt2">立即注册</a>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {

    };
  },
};
</script>
