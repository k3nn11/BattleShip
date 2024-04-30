using System;
using Application.DTO;
using BattleShip.Field;

namespace Application.ModelMapper
{
    public static class PlayingFieldMapper
    {
        public static GetFieldDTO GetFieldDTO(PlayingField playingField)
        {
            return new GetFieldDTO
            {
                Id = playingField.Id,
                FieldName = playingField.Name,
                Height = playingField.Height,
                Width = playingField.Width,
            };
        }

        public static FieldStateDTO GetFieldStateDTO(PlayingField playingField)
        {
            return new FieldStateDTO
            {
                Id = playingField.Id,
                Ships = playingField.Ships,
                FieldName = playingField.Name,
                Height = playingField.Height,
                Width = playingField.Width,
            };
        }

        public static PlayingField MapToModel(PostPlayingFieldDTO playingFieldDTO)
        {
            return new PlayingField(playingFieldDTO.Height, playingFieldDTO.Width)
            {
                Name = playingFieldDTO.FieldName,
                Height = playingFieldDTO.Height,
                Width = playingFieldDTO.Width,
            };
        }
    }
}
