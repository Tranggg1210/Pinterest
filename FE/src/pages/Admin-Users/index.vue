<script setup>
import { h, ref } from 'vue'
import { NButton, NDataTable } from 'naive-ui'

const columns = [
  {
    title: 'Name',
    key: 'name'
  },
  {
    title: 'Age',
    key: 'age',
    sorter: (row1, row2) => row1.age - row2.age
  },
  {
    title: 'Chinese Score',
    key: 'chinese',
    defaultSortOrder: false,
    sorter: {
      compare: (a, b) => a.chinese - b.chinese,
      multiple: 3
    }
  },
  {
    title: 'Math Score',
    defaultSortOrder: false,
    key: 'math',
    sorter: {
      compare: (a, b) => a.math - b.math,
      multiple: 2
    }
  },
  {
    title: 'English Score',
    defaultSortOrder: false,
    key: 'english',
    sorter: {
      compare: (a, b) => a.english - b.english,
      multiple: 1
    }
  },
  {
    title: 'Address',
    key: 'address',
  },
  {
    title: 'Action',
    key: 'actions',
    render (row) {
      return h(
        NButton,
        {
          strong: true,
          tertiary: true,
          size: 'small',
          onClick: () => play(row)
        },
        { default: () => 'Play' }
      )
    }
  }
]

const data = [
  {
    key: 0,
    name: 'John Brown',
    age: 32,
    address: 'New York No. 1 Lake Park',
    chinese: 98,
    math: 60,
    english: 70
  },
  {
    key: 1,
    name: 'Jim Green',
    age: 42,
    address: 'London No. 1 Lake Park',
    chinese: 98,
    math: 66,
    english: 89
  },
  {
    key: 2,
    name: 'Joe Black',
    age: 32,
    address: 'Sidney No. 1 Lake Park',
    chinese: 98,
    math: 66,
    english: 89
  },
  {
    key: 3,
    name: 'Jim Red',
    age: 32,
    address: 'London No. 2 Lake Park',
    chinese: 88,
    math: 99,
    english: 89
  }
]

const dataTableInstRef = ref(null)
const pagination = ref({ pageSize: 10 })

const filterAddress = () => {
  dataTableInstRef.value.filter({
    address: ['London']
  })
}

const sortName = () => {
  dataTableInstRef.value.sort('name', 'ascend')
}

const clearFilters = () => {
  dataTableInstRef.value.filter(null)
}

const clearSorter = () => {
  dataTableInstRef.value.sort(null)
}
</script>

<template>
  <div class="admin-users">
    <n-data-table
      ref="dataTableInst"
      :columns="columns"
      :data="data"
      :pagination="pagination"
    />
  </div>
</template>

<style lang="scss" scoped>
.admin-users
{
  padding: 40px;
}
</style>
<route lang="yaml">
name: Admin-Users
meta:
  layout: admin
  requiresAuth: true
</route>
