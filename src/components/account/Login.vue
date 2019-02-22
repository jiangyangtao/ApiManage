<template>
  <div class="login-container">
    <div class="wrap-login">
      <span class="login-title">登录</span>
      <div class="error-tip">{{ errorText }}</div>
      <div class="wrap-input username" data-validate="请输入用户名">
        <span class="label-input">邮箱</span>
        <input ref="username" class="input" type="text" v-model="username" placeholder="请输入邮箱">
        <span class="focus-input" data-symbol=""></span>
      </div>
      <div class="wrap-input">
        <span class="label-input">密码</span>
        <input ref="password" class="input" type="password" v-model="password" placeholder="请输入密码">
        <span class="focus-input" data-symbol=""></span>
      </div>
      <div class="forget-password">
        <a href="javascript:">忘记密码？</a>
      </div>
      <div class="login-btn-container">
        <div class="wrap-login-btn">
          <div class="login-bgbtn"></div>
          <button class="login-btn" @click="login">登 录</button>
        </div>
      </div>
      <div class="register">
        <a href="javascript:">立即注册</a>
      </div>
    </div>
  </div>
</template>

<script>
import cookie from 'js-cookie';

export default {
  data() {
    return {
      username: '',
      password: '',
      errorText: '',
    };
  },
  methods: {
    login() {
      if (this.validate()) {
        this.$server.account.login({
          email: this.username,
          password: this.password,
        }, { notNotify: true }).then((response) => {
          cookie.set('authorization', response.Data);
        }).catch((error) => { this.errorText = error; });
      }
    },
    validate() {
      if (!this.username) {
        this.$refs.username.focus();
        return false;
      }
      if (!this.password) {
        this.$refs.password.focus();
        return false;
      }
      return true;
    },
  },
};
</script>
