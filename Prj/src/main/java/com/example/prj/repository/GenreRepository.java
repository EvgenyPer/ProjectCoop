package com.example.prj.repository;

import com.example.prj.entity.Genre;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository // Аннотация указывает, что этот интерфейс является репозиторием Spring.
public interface GenreRepository extends JpaRepository<Genre, Long> { // Наследуется от JpaRepository для стандартных операций.
    // JpaRepository предоставляет готовые методы для работы с сущностью Genre (CRUD-операции).
}
