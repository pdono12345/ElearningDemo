namespace ElearningDemoRepositories.IRepositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    ///     回傳 IQueryable 讓 Service 可以處理較複雜的查詢
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity>? Query();
    /// <summary>
    /// 回傳所有 data 沒有做任何篩選
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity>? GetAll();
    /// <summary>
    ///     回傳所有 data 沒有任何篩選
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>?> GetAllAsync();
    /// <summary>
    ///     須 override，回傳 IsValid = true && IsDeleted = false 的所有 data
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>?> GetAllValidAsync();
    /// <summary>
    ///     須 override，回傳 IsDeleted = false 的所有 data
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>?> GetAllAliveAsync();
    /// <summary>
    /// 搜尋單筆資料，可輸入條件表示式
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> predicate);
    /// <summary>
    /// 搜尋單筆資料，可輸入條件表示式
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    TEntity? GetOne(Expression<Func<TEntity, bool>> predicate);
    /// <summary>
    /// 預設 insert 之後回傳 null
    /// </summary>
    /// <param name="tEntity"></param>
    /// <param name="creatorId"></param>
    /// <returns>tEntityId</returns>
    void Create(TEntity tEntity);
    /// <summary>
    /// update 一筆 data
    /// </summary>
    /// <param name="tEntity"></param>
    /// <param name="editorId"></param>
    void Update(TEntity tEntity);
    /// <summary>
    /// 軟刪除，修改狀態 IsDeleted = true，必須 override
    /// </summary>
    /// <param name="id"></param>
    /// <param name="editorId"></param>
    void SoftDelete(int id);
    void HardDelete(TEntity tEntity);
    Task SaveChangeAsync();
    void SaveChange();
    /// <summary>
    ///     update 之前先確認是否已經有同 id 被追蹤，有的話 call 這個 method 會消除追蹤
    /// </summary>
    /// <param name="condition"></param>
    void DetachedTrackByCondition(Func<TEntity, bool> condition);
}

