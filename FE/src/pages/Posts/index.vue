<script setup>
import { ref,reactive } from 'vue';
 const posts = reactive({
    title: null,
    description: null,
    url: null,
    table: null,
    taggedTopic: null,
    option: null,
 });
 const formRef = ref(null);
 const generalOptions = ref([
    { label: 'groode', value: 'groode' },
    { label: 'veli good', value: 'veli good' },
    { label: 'emazing', value: 'emazing' },
    { label: 'lidiculous', value: 'lidiculous' }
  ])
</script>

<template >
    <div class="container posts">
        <div class="wide">
            <h1 class="posts-title">Tạo bài viết</h1>
            <div class="handle-posts">
                <div class="image-uploaded">
                    <n-upload
                        multiple
                        directory-dnd
                        action="https://www.mocky.io/v2/5e4bafc63100007100d8b70f"
                        :max="5"
                    >
                        <n-upload-dragger>
                        <div style="margin-bottom: 12px">
                            <IconUpload size="40" style="opacity: 0.3;"/>
                        </div>
                        <n-text style="font-size: 16px">
                            Chọn một tệp hoặc kéo thả ở đây
                        </n-text>
                        <n-p depth="3" style="margin: 8px 0 0 0">
                            Bạn nên sử dụng tập tin .jpg chất lượng cao có kích thước dưới 20MB hoặc tập tin .mp4 chất lượng cao có kích thước dưới 200MB
                        </n-p>
                        </n-upload-dragger>
                    </n-upload>
                </div>
                <div class="infor-uploaded">
                    <n-form ref="formRef" :model="posts">
                        <n-form-item path="title" label="Tiêu đề">
                            <n-input v-model:value="posts.title" placeholder="Thêm tiêu đề" class="posts-input" />
                        </n-form-item>
                        <n-form-item path="description" label="Mô tả">
                            <n-input
                                v-model:value="posts.description"
                                placeholder="Thêm mô tả chi tiết"
                                type="textarea" 
                                class="posts-input"
                                :autosize="{
                                    minRows: 3,
                                    maxRows: 5
                                }"
                            />
                        </n-form-item>
                        <n-form-item path="url" label="Liên kết">
                            <n-input v-model:value="posts.url" placeholder="Thêm liên kết" class="posts-input"/>
                        </n-form-item>
                        <n-form-item path="table" label="Bảng">
                            <n-select
                                class="posts-input"
                                v-model:value="posts.table"
                                placeholder="Chọn bảng"
                                :options="generalOptions"
                                multiple
                            />
                        </n-form-item>
                        <n-form-item path="taggedTopic" label="Chủ đề được gắn thẻ">
                            <n-input v-model:value="posts.taggedTopic" placeholder="Tìm kiếm thẻ" class="posts-input" />
                        </n-form-item>
                        <n-form-item class="btn-container">
                            <HfButton>Đăng</HfButton>
                        </n-form-item>
                    </n-form>
                </div>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped src="./Posts.scss"></style>
