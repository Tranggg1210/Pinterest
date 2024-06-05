<script setup></script>
<template>
  <section className="features container-center">
    <div className="width-page">
      <div className="feature-item">
        <IconBulb class="feature-icon" size="60" />
        <h1 className="h1-title">Tìm kiếm ý tưởng</h1>
        <p className="p-desc">
          Thông qua website này các designer có thể tìm được cảm hứng sáng tác!
        </p>
      </div>
      <div className="feature-item">
        <IconDownload class="feature-icon" size="60" />
        <h1 className="h1-title">Lưu ý tưởng bạn yêu thích</h1>
        <p className="p-desc">Thu thập nội dung bạn yêu thích để bạn có thể quay lại xem sau.</p>
      </div>
      <div className="feature-item">
        <IconRadioactive class="feature-icon" size="60" />
        <h1 className="h1-title">Xem, làm, thử, thực hiện</h1>
        <p className="p-desc">
          Điều tuyệt nhất là khám phá những nội dung và ý tưởng mới từ mọi người khắp thế giới.
        </p>
      </div>
    </div>
  </section>
</template>

<style lang="scss" scoped>
.features {
  background-color: #f8f9fa;
  padding: 7rem 0;
  .width-page {
    @include flex(space-between);
    @include mobile {
      flex-direction: column;
    }
    @include small-tablet {
      flex-direction: column;
    }
  }
  @include mobile {
    padding: 1rem;
  }
}
.feature-item {
  width: 33%;
  padding: 0 2.5rem;
  @include flex;
  flex-direction: column;
  .feature-icon {
    color: $primary-color;
    margin: 0 0 1rem;
    @include mobile {
      font-size: 3rem;
    }
  }
  @include mobile {
    width: 100%;
    padding: 1rem 0;
    h1 {
      text-align: center;
    }
  }
  @include small-tablet {
    width: 100%;
    padding: 2rem 0;
    h1 {
      text-align: center;
    }
  }
  @include tablet {
    h1 {
      text-align: center;
    }
  }
}
</style>
