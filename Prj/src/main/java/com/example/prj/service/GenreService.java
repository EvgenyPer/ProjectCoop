package com.example.prj.service;

import com.example.prj.entity.Genre;
import com.example.prj.repository.GenreRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service // Аннотация указывает, что этот класс является сервисом Spring.
public class GenreService {

    private final GenreRepository genreRepository; // Репозиторий для взаимодействия с базой данных.

    // Конструктор с внедрением зависимости GenreRepository.
    public GenreService(GenreRepository genreRepository) {
        this.genreRepository = genreRepository;
    }

    /**
     * Возвращает список всех жанров.
     * @return Список объектов Genre.
     */
    public List<Genre> getAllGenres() {
        return genreRepository.findAll(); // Вызывает метод репозитория для получения всех записей.
    }

    /**
     * Возвращает жанр по его идентификатору.
     * @param id Идентификатор жанра.
     * @return Объект Genre, если найден.
     */
    public Optional<Genre> getGenreById(Long id) {
        return genreRepository.findById(id); // Вызывает метод репозитория для поиска жанра по id.
    }

    /**
     * Сохраняет новый жанр в базе данных.
     * @param genre Объект Genre для сохранения.
     * @return Сохраненный объект Genre.
     */
    public Genre saveGenre(Genre genre) {
        return genreRepository.save(genre); // Вызывает метод репозитория для сохранения объекта.
    }

    /**
     * Обновляет существующий жанр.
     * @param id Идентификатор жанра.
     * @param updatedGenre Обновленные данные жанра.
     * @return Обновленный объект Genre.
     */
    public Genre updateGenre(Long id, Genre updatedGenre) {
        return genreRepository.findById(id) // Ищет жанр по id.
                .map(existingGenre -> { // Если жанр найден, обновляет его данные.
                    existingGenre.setName(updatedGenre.getName());
                    return genreRepository.save(existingGenre); // Сохраняет изменения.
                })
                .orElseThrow(() -> new RuntimeException("Genre not found")); // Выбрасывает исключение, если жанр не найден.
    }

    /**
     * Удаляет жанр по его идентификатору.
     * @param id Идентификатор жанра.
     */
    public void deleteById(Long id) {
        genreRepository.deleteById(id); // Вызывает метод репозитория для удаления жанра по id.
    }
}
