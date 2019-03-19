<template>
<el-container class="register-container">
  <el-form ref="form" :model="form" :rules="rules" label-width="80px">
    <h2 class="register-title">用户注册</h2>
    <el-form-item label="邮箱" prop="email">
      <el-autocomplete ref="emailInput"
        class="email"
        clearable
        v-model="form.email"
        :fetch-suggestions="querySearch"
        placeholder="请输入邮箱"
        :trigger-on-focus="false"
        @select="emailSelected"
        ></el-autocomplete>
    </el-form-item>
    <el-form-item label="姓名" prop="name">
      <el-input v-model="form.name" clearable placeholder="请输入姓名"></el-input>
    </el-form-item>
    <el-form-item label="性别">
      <el-radio-group v-model="form.gender">
        <el-radio label="1">男</el-radio>
        <el-radio label="0">女</el-radio>
      </el-radio-group>
    </el-form-item>
    <el-form-item label="密码" prop="password">
      <el-input type="password" ref="passwordInput" v-model="form.password" placeholder="请输入密码" @focus="passwordFocus" @blur="passwordBlur">
        <template slot="append"><el-button @click="createPassword">推荐密码</el-button></template>
      </el-input>
    </el-form-item>
    <el-form-item>
      <el-button @click="cancel">取消</el-button>
      <el-button type="primary" @click="submit('form')">立即注册</el-button>
    </el-form-item>
  </el-form>
</el-container>
</template>
<script>

import { passwordGenerator } from 'javascript-commons';

export default {
  data() {
    return {
      form: {
        email: '',
        name: '',
        gender: '1',
        password: '',
      },
      emailSuffixs: [
        { value: '@qq.com' },
        { value: '@126.com' },
        { value: '@163.com' },
        { value: '@sina.com' },
        { value: '@foxmail.com' },
        { value: '@outlook.com' },
        { value: '@139.com' },
        { value: '@189.com' },
        { value: '@aliyun.com' },
        { value: '@gamil.com' },
      ],
      rules: {
        email: [
          { required: true, message: '请输入邮箱', trigger: 'blur' },
          { type: 'email', required: true, message: '请输入正确的邮箱', trigger: 'blur' },
        ],
        name: [
          { required: true, message: '请输入姓名', trigger: 'blur' },
        ],
        password: [
          { required: true, message: '请输入密码', trigger: 'blur' },
        ],
      },
    };
  },
  mounted() {

  },
  methods: {
    submit(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.$server.account.register(this.form).then((response) => {
            this.$alert(response.Data, '成功', {
              confirmButtonText: '确定',
              type: 'success',
              callback: (action) => {
                this.$router.push({
                  name: 'login',
                });
              },
            });
          });
        }
      });
    },
    cancel() {
      this.$router.push({
        name: 'login',
      });
    },
    querySearch(queryString, callback) {
      if (!queryString) return;
      const results = this.emailSuffixs.map(item => ({ value: `${queryString}${item.value}` }));
      callback(results);
    },
    emailSelected() {
      this.$refs.emailInput.$refs.input.$refs.input.focus();
      this.$refs.emailInput.$refs.input.$refs.input.blur();
    },
    passwordFocus() {
      this.$refs.passwordInput.$refs.input.type = 'text';
    },
    passwordBlur() {
      this.$refs.passwordInput.$refs.input.type = 'password';
    },
    createPassword() {
      this.form.password = passwordGenerator.newPassword(12, false, true);
    },
  },
};
</script>
