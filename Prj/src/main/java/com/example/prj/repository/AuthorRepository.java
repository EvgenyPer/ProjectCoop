package com.example.prj.repository;

// Импорт сущности Author
import com.example.prj.entity.Author;
// Импорт интерфейса JpaRepository для работы с CRUD-операциями
import org.springframework.data.jpa.repository.JpaRepository;
// Аннотация @Repository указывает, что это репозиторий Spring
import org.springframework.stereotype.Repository;

@Repository
// Интерфейс AuthorRepository расширяет JpaRepository, позволяя работать с Author
// JpaRepository параметризуется сущностью (Author) и типом её ID (Long)
public interface AuthorRepository extends JpaRepository<Author, Long> {
}
