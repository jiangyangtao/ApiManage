<template>
  <div class="login-container">
    <div class="wrap-login">
      <span class="login-title">登录</span>
      <div class="error-tip">{{ errorText }}</div>
      <div class="wrap-input email">
        <span class="label-input">邮箱</span>
        <input ref="email" class="input" @blur="validateEmail" type="text" v-model="email" placeholder="请输入邮箱">
        <span class="focus-input" data-symbol=""></span>
      </div>
      <div class="wrap-input">
        <span class="label-input">密码</span>
        <input ref="password" class="input" type="password" v-model="password" placeholder="请输入密码">
        <span class="focus-input" data-symbol=""></span>
      </div>
      <div v-show="loginFailCount >= 3" class="wrap-input verification-code-wrap">
        <span class="label-input">验证码</span>
        <input ref="verificationCode" class="input" type="text" v-model="inputVerificationCode" placeholder="请输入验证码">
        <span class="focus-input" data-symbol=""></span>
        <verification-code @codeChange="codeChange" :fontSizeMin="34"></verification-code>
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
        <a href="javascript:" @click="register">立即注册</a>
      </div>
    </div>
  </div>
</template>

<script>
import cookie from 'js-cookie';
import VerificationCode from './../common/VerificationCode';

export default {
  components: {
    VerificationCode,
  },
  data() {
    return {
      email: '',
      password: '',
      errorText: '',
      loginFailCount: 0,
      verificationCode: '',
      inputVerificationCode: '',
    };
  },
  created() {
    const failCount = localStorage.getItem('failCount');
    if (failCount) this.loginFailCount = failCount.toInt();
    if (cookie.get('authorization')) {
      this.$server.home.authorization().then((response) => {
        if (response.Code === 0) {
          this.$router.push({
            name: 'index',
          });
        }
      });
    }
  },
  methods: {
    login() {
      if (this.validate()) {
        this.$server.account.login({
          email: this.email,
          password: this.password,
        }, { notNotify: true }).then((response) => {
          cookie.set('authorization', response.Data);
          localStorage.setItem('failCount', 0);
          this.$router.push({
            name: 'index',
          });
        }).catch((error) => {
          this.loginFailCount++;
          localStorage.setItem('failCount', this.loginFailCount);
          this.errorText = error;
        });
      }
    },
    validate() {
      if (!this.email) {
        this.$refs.email.focus();
        return false;
      }
      if (!this.validateEmail()) return false;
      if (!this.password) {
        this.$refs.password.focus();
        return false;
      }
      if (this.loginFailCount >= 3) {
        if (!this.inputVerificationCode) {
          this.$refs.verificationCode.focus();
          return false;
        }

        if (this.inputVerificationCode !== this.verificationCode) {
          this.errorText = '验证码错误，请重新输入！';
          return false;
        }
      }
      this.errorText = '';
      return true;
    },
    validateEmail() {
      if (this.email && !this.email.isEmail()) {
        this.errorText = '请输入正确的邮箱！';
        return false;
      }
      this.errorText = '';
      return true;
    },
    register() {
      this.$router.push({
        name: 'register',
      });
    },
    codeChange(val) {
      this.verificationCode = val;
    },
  },
};
</script>
