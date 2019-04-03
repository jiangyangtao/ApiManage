<template>
  <div class="verification-code" @click="changeCode">
    <canvas id="verification-canvas" :width="contentWidth" :height="contentHeight"></canvas>
  </div>
</template>
<script>
export default {
  name: 'VerificationCode',
  data() {
    return {
      verificationCode: '',
      verificationChar: '1234567890abcdefghijklmnopqrstuvwxyz',
    };
  },
  props: {
    codeNumber: {
      type: Number,
      default: 4,
    },
    fontSizeMin: {
      type: Number,
      default: 16,
    },
    fontSizeMax: {
      type: Number,
      default: 40,
    },
    backgroundColorMin: {
      type: Number,
      default: 180,
    },
    backgroundColorMax: {
      type: Number,
      default: 240,
    },
    colorMin: {
      type: Number,
      default: 50,
    },
    colorMax: {
      type: Number,
      default: 160,
    },
    lineNumber: {
      type: Number,
      default: 6,
    },
    lineColorMin: {
      type: Number,
      default: 40,
    },
    lineColorMax: {
      type: Number,
      default: 180,
    },
    dotNumber: {
      type: Number,
      default: 80,
    },
    dotColorMin: {
      type: Number,
      default: 0,
    },
    dotColorMax: {
      type: Number,
      default: 255,
    },
    contentWidth: {
      type: Number,
      default: 112,
    },
    contentHeight: {
      type: Number,
      default: 38,
    },
  },
  watch: {
    verificationCode() {
      this.drawPic();
    },
  },
  created() {
    this.createCode();
  },
  methods: {
    randomColor(min, max) {
      const r = Math.randomRange(min, max);
      const g = Math.randomRange(min, max);
      const b = Math.randomRange(min, max);
      return `rgb(${r},${g},${b})`;
    },
    drawPic() {
      const canvas = document.getElementById('verification-canvas');
      const ctx = canvas.getContext('2d');
      ctx.textBaseline = 'bottom';
      ctx.fillStyle = this.randomColor(this.backgroundColorMin, this.backgroundColorMax);
      ctx.fillRect(0, 0, this.contentWidth, this.contentHeight);

      for (let i = 0; i < this.verificationCode.length; i++) {
        this.drawText(ctx, this.verificationCode[i], i);
      }
      this.drawLine(ctx);
      this.drawDot(ctx);
    },
    drawText(ctx, txt, i) {
      /* eslint no-param-reassign: "error" */
      ctx.fillStyle = this.randomColor(this.colorMin, this.colorMax);
      ctx.font = `${Math.randomRange(this.fontSizeMin, this.fontSizeMax)}px SimHei`;
      const x = (i + 1) * (this.contentWidth / (this.verificationCode.length + 1));
      const y = Math.randomRange(this.fontSizeMax, this.contentHeight - 5);
      const deg = Math.randomRange(-45, 45);
      ctx.translate(x, y);
      ctx.rotate((deg * Math.PI) / 180);
      ctx.fillText(txt, 0, 0);
      ctx.rotate((-deg * Math.PI) / 180);
      ctx.translate(-x, -y);
    },
    drawLine(ctx) {
      for (let i = 0; i < this.lineNumber; i++) {
        ctx.strokeStyle = this.randomColor(this.lineColorMin, this.lineColorMax);
        ctx.beginPath();
        ctx.moveTo(Math.randomRange(0, this.contentWidth), Math.randomRange(0, this.contentHeight));
        ctx.lineTo(Math.randomRange(0, this.contentWidth), Math.randomRange(0, this.contentHeight));
        ctx.stroke();
      }
    },
    drawDot(ctx) {
      for (let i = 0; i < this.dotNumber; i++) {
        ctx.fillStyle = this.randomColor(0, 255);
        ctx.beginPath();
        ctx.arc(Math.randomRange(0, this.contentWidth), Math.randomRange(0, this.contentHeight), 1, 0, 2 * Math.PI);
        ctx.fill();
      }
    },
    createCode() {
      let code = '';
      for (let i = 0; i < this.codeNumber; i++) {
        code += this.verificationChar[Math.randomRange(0, this.verificationChar.length)];
      }
      this.$emit('codeChange', code);
      this.verificationCode = code;
    },
    changeCode() {
      this.createCode();
    },
  },
};
</script>
