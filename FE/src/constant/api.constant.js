export const ApiConstant = {
  auth: {
    login: 'Accounts/signIn',
    register: 'Accounts/signUp',
    changePassword: 'Accounts/changePassword'
  },
  post: {
    postAll: 'Posts/getAll',
    createPost: 'Posts/create',
    postById: 'Posts/getById',
    postByUserId: 'Posts/getByUserId',
    deletePostById: 'Posts/delete',
    updatePostById: 'Posts/update',
    getByCollectionId: 'Posts/getByCollectionId'
  },
  user: {
    currentUser: 'Users/getLoginUser',
    changeAvatar: 'Users/avatar',
    changeInfor: 'Users/profile',
    deleteAccount: 'Users/delete',
    getUserById: 'Users/getById',
    followUser: 'Follow/follower',
    unFollowUser: 'Follow/unfollower',
    checkFollow: 'Follow/checkFollower'
  },
  collection: {
    collectionAll: 'Posts/getAll',
    createCollection: 'Collections/create',
    getCollectionByUserId: 'Collections/getByUserId',
    getCollectionByPostId: 'Collections/getByPostId',
    savePost: "Posts/toggleCollection",
    getById: 'Collections/getById',
    updateBackgound: 'Collections/background',
    updateCollection: 'Collections/update',
    deleteCollection: 'Collections/delete'
  }
};
